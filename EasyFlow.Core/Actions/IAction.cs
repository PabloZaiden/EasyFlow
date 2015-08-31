using System;
using Newtonsoft.Json.Linq;

namespace EasyFlow.Core
{
	public interface IAction
	{
		void Execute (Workflow workflow, Transition transition, String user, JToken operationData, params String[] parameters);
	}
}

