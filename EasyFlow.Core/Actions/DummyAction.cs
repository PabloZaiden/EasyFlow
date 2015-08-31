using System;
using Newtonsoft.Json.Linq;

namespace EasyFlow.Core
{
	public class DummyAction : IAction
	{
		public DummyAction ()
		{
		}

		#region IAction implementation

		public void Execute (Workflow workflow, Transition transition, string user, JToken operationData, params string[] parameters)
		{
			Console.WriteLine ("WF: " + workflow.Id);
			Console.WriteLine ("TR to: " + transition.StateName);
			foreach (var p in parameters) {
				Console.WriteLine (p);
			}}

		#endregion
	}
}

