using System;
using System.Collections.Generic;

namespace EasyFlow.Core
{
	public class Transition
	{
		public Transition ()
		{
			Rules = new List<Command> ();
			Actions = new List<Command> ();
		}

		public String Name {
			get;
			set;
		}

		public String StateName {
			get;
			set;
		}

		public String Permission {
			get;
			set;
		}

		public List<Command> Rules {
			get;
			set;
		}

		public List<Command> Actions {
			get;
			set;
		}
	}
}

