using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasyFlow.Core
{
	public class Workflow
	{
		private object _sync = new object();

		#region Constructors

		public Workflow () 
			: this(DummyAuthManager.Instance)
		{
		}

		public Workflow(IAuthManager authManager) {
			AuthManager = authManager;
			States = new List<State> ();
			LastModified = DateTime.Now;
		}

		#endregion

		#region Serialization

		public static Workflow Create(String workflowData) {
			var workflow = JsonConvert.DeserializeObject<Workflow> (workflowData);
			return workflow;
		}

		public void Load(String workflowData) {
			lock (_sync) {
				JsonConvert.PopulateObject (workflowData, this);
			}
		}

		public String Dump() {
			return JsonConvert.SerializeObject (this);
		}

		#endregion

		#region Public Methods

		public List<ValidationError> ValidateWorkflow() {
			List<ValidationError> errors = new List<ValidationError> ();

			if (!States.Any (s => s.Name == CurrentState)) {
				errors.Add (new ValidationError ("Invalid current state"));
			}

			foreach (var state in States) {
				foreach (var t in state.Transitions) {
					
					if (!States.Any (s => s.Name == t.StateName)) {
						String message = "Invalid transition to state " + t.StateName + " from state " + state.Name;
						errors.Add (new ValidationError (message));
					}



					foreach (var r in t.Rules) {
						var rule = CommandManager.GetCommand<IRule> (r.Name);
						if (rule == null) {
							errors.Add (new ValidationError ("Invalid rule: " + r.Name));
						}
					}

					foreach (var a in t.Actions) {
						var rule = CommandManager.GetCommand<IAction> (a.Name);
						if (rule == null) {
							errors.Add (new ValidationError ("Invalid action: " + a.Name));
						}
					}


					if (!AuthManager.PermissionIsValid (t.Permission)) {
						errors.Add (new ValidationError ("Invalid permission " + t.Permission));
					}
				}
			}	
				
			return errors;
		}

		public List<ValidationError> ValidateTransition(String user, String newState)
		{
			List<ValidationError> errors = new List<ValidationError> ();

			var state = States.Single (s => s.Name == CurrentState);
			var transition = state.Transitions.FirstOrDefault (t => t.StateName == newState);

			if (transition == null) {
				errors.Add(new ValidationError("Invalid transition"));
				return errors;
			}

			if (!AuthManager.UserHasPermission(user, transition.Permission)) {
				errors.Add(new PermissionValidation(user, transition.Permission));
				return errors;
			}

			foreach (var r in transition.Rules) {
				var rule = CommandManager.GetCommand<IRule>(r.Name);
				if (rule == null) {
					errors.Add (new ValidationError ("Invalid rule: " + r.Name));
				}

				try {
					var ruleErrors = rule.GetValidationErrors (this, transition, r.Parameters.ToArray ());
					errors.AddRange(ruleErrors);
				} catch (Exception ex) {
					Logger.Log (LogLevel.Warning, "Error verifying rule {0} for transition from {1} to {2} on workflow {3}",
						r.Name, CurrentState, newState, Id);
					Logger.Log (ex);
					errors.Add (new ValidationError ("Error executing rule " + r.Name));
				}
				
			}

			return errors;
		}

		public void Transition(String user, String newState, JToken operationData) {
			lock (_sync) {
				var errors = ValidateTransition (user, newState);

				if (errors.Count > 0) {
					var ex = new ValidationException (errors);
					Logger.Log (ex);
					throw ex;
				}

				var transition = States
				.Single (s => s.Name == CurrentState)
				.Transitions.Single (t => t.StateName == newState); 

				foreach (var a in transition.Actions) {
					var action = CommandManager.GetCommand<IAction> (a.Name);
					if (action != null) {
						try {

							action.Execute (this, transition, user, operationData, a.Parameters.ToArray ());
						} catch (Exception ex) {
							Logger.Log (LogLevel.Warning, "Error executing action {0} transitioning from {1} to {2} on workflow {3}",
								a.Name, CurrentState, newState, Id);
					
							Logger.Log (ex);
						}
					}
				}

				var oldState = _currentState;
				_currentState = newState;
				LastModified = DateTime.Now;
				Logger.Log (LogLevel.Info, "Workflow {0} transitioned from {1} to {2} by {3}", Id, oldState, newState, user ?? "<nobody>");
			}
		}

		public List<Transition> GetTransitionsForCurrentState() {
			return States.Single (s => s.Name == CurrentState).Transitions;
		}

		public bool TryAutomaticTransition() {
			var availableTransitions = GetTransitionsForCurrentState ();
			if (availableTransitions.Count != 1) {
				return false;
			}
			Transition transition = availableTransitions [0];

			if (ValidateTransition (null, transition.StateName).Count != 0) {
				return false;
			}

			Transition (null, transition.StateName, null);
			return true;
		}
			
		#endregion

		#region Public Properties

		public JToken Data {
			get;
			set;
		}

		public Guid Id {
			get;
			set;
		}

		public List<State> States {
			get;
			private set;
		}

		[JsonProperty("CurrentState")]
		private String _currentState;

		[JsonIgnore]
		public String CurrentState {
			get {
				return _currentState;
			}
		}

		public DateTime LastModified {
			get;
			set;
		}

		#endregion

		#region Managers

		internal IAuthManager AuthManager {
			get;
			set;
		}

		#endregion

		#region Internal Methods

		#endregion

	}
}

