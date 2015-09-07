using System;

namespace EasyFlow.Core
{
	public interface ICondition
	{
		bool IsMetByWorkflow (Workflow workflow, params String[] parameters);
	}
}

