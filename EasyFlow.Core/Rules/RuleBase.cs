using System;
using System.Linq;
using System.Collections.Generic;

namespace EasyFlow.Core
{
	public abstract class RuleBase : IRule
	{
		public RuleBase ()
		{
		}

		#region IRule implementation
		public bool IsValid (Workflow workflow, Transition transition, params string[] parameters)
		{
			return GetValidationErrors(workflow, transition, parameters).Count == 0;
		}

		public abstract List<ValidationError> GetValidationErrors (Workflow workflow, Transition transition, params string[] parameters);

		#endregion
	}
}

