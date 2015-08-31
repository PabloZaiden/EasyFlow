using System;
using System.Collections.Generic;

namespace EasyFlow.Core
{
	public class State
	{
		public State ()
		{
			Transitions = new List<Transition> (); 
		}

		public String Name {
			get;
			set;
		}

		public List<Transition> Transitions { 
			get; 
			set;
		}
	}
}

