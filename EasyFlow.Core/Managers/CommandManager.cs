using System;
using System.Linq;

namespace EasyFlow.Core
{
	public class CommandManager
	{
		public CommandManager ()
		{
		}

		static Type GetInternalCommand (string name)
		{
			var type = typeof(CommandManager).Assembly.GetTypes ()
				.FirstOrDefault (t => t.Name == name);

			return type;
		}

		public static T GetCommand<T>(String name) where T : class {

			Type type = null;
			if (!name.Contains (".")) {
				type = GetInternalCommand (name);
			} else {
				type = Type.GetType (name, false);
			}

			if (type == null) { 
				return null;
			}


			return Activator.CreateInstance (type) as T;
		}
	}
}

