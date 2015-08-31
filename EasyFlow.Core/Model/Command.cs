using System;
using System.Collections.Generic;

namespace EasyFlow.Core
{
	public class Command
	{
		public Command ()
		{
			Parameters = new List<string> ();
		}

		public String Name {
			get;
			set;
		}

		public List<String> Parameters {
			get;
			set;
		}
	}
}

