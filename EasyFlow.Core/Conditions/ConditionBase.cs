using System;

namespace EasyFlow.Core
{
	public abstract class ConditionBase : ICondition
	{
		public ConditionBase ()
		{
		}

		protected Object GetWorkflowParameter(Workflow workflow, String parameter) { 
			throw new NotImplementedException();
		}

		#region ICondition implementation

		public abstract bool IsMetByWorkflow (Workflow workflow, params string[] parameters);

		#endregion
	}
}

