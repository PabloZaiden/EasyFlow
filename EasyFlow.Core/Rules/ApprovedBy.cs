using System;
using System.Collections.Generic;

namespace EasyFlow.Core
{
	public class ApprovedBy : RuleBase
	{
		public ApprovedBy ()
		{
		}

		#region IRule implementation

		public override List<ValidationError> GetValidationErrors (Workflow workflow, Transition transition, params string[] parameters)
		{
			List<ValidationError> errors = new List<ValidationError> ();

			var usernamesForApproval = parameters;

			IAuthManager auth = workflow.AuthManager;

			foreach (var username in usernamesForApproval) {
				if (!auth.UserHasApprovedStateTransition(
					workflow.Id,
					workflow.CurrentState, //state from
					transition.StateName, //state to
					username)) {

					errors.Add (new UserValidationError (username));
				}
			}

			return errors;
		}

		#endregion
	}
}

