using System;

namespace EasyFlow.Core
{
	public class Equals : ICondition
	{
		public Equals ()
		{
		}

		#region ICondition implementation
		public bool IsMetByWorkflow (Workflow workflow, params string[] parameters)
		{
			throw new NotImplementedException ();
		}
		#endregion
	}
}

