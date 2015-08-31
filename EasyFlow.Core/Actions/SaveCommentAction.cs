using System;
using Newtonsoft.Json.Linq;

namespace EasyFlow.Core
{
	public class SaveCommentAction : IAction
	{
		public SaveCommentAction ()
		{
		}

		#region IAction implementation

		public void Execute (Workflow workflow, Transition transition, string user, JToken operationData, params string[] parameters)
		{
			if (operationData != null && operationData ["comment"] != null) {
				Console.WriteLine (String.Format (
					"User {0} commented '{1}' while transitioning from {2} to {3}",
					user, 
					operationData ["comment"].Value<String> (), 
					workflow.CurrentState, 
					transition.StateName));
			}
		}

		#endregion
	}
}

