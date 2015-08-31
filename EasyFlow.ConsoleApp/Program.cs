using System;
using EasyFlow.Core;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace EasyFlow.ConsoleApp
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var data = File.ReadAllText("/Users/PabloZaiden/Desktop/EasyFlow/data.json");
			var workflow = Workflow.Create (data);


			Console.WriteLine ("Loaded");
			Line();

			var errors = workflow.ValidateWorkflow ();

			ValidateErrors (errors);

			Console.WriteLine ("Validated");
			Line ();

			DummyAuthManager.Instance.ApproveStateTransition (workflow.Id, workflow.CurrentState, "State2", "U1");
			DummyAuthManager.Instance.ApproveStateTransition (workflow.Id, workflow.CurrentState, "State2", "U2");


			Console.WriteLine ("Transition Approved");
			Line ();

			errors = workflow.ValidateTransition ("bananaJay", "State2");
			ValidateErrors (errors);

			Console.WriteLine ("Transition Validated");
			Line();

			Console.WriteLine (workflow.Dump());
			Console.WriteLine ("Dumped");
			Line ();

			var comment = new JObject ();
			comment ["comment"] = "Everything is awesome!";

			workflow.Transition ("U3", "State2", comment);
			Console.WriteLine ("Transition done!");
			Line ();
			Console.WriteLine (workflow.Dump());
			Console.WriteLine ("Dumped");
			Line ();

			//comment ["comment"] = "Everything is awesome again!";

			//workflow.Transition ("U3", "State3", comment);

			var autoTransition = workflow.TryAutomaticTransition ();
			Console.WriteLine ("AutoTransition: " + autoTransition);
			Line ();
			Console.WriteLine (workflow.Dump());
			Console.WriteLine ("Dumped");
			Line ();


		}

		static void ValidateErrors (List<ValidationError> errors)
		{
			if (errors.Count > 0) {
				foreach (var error in errors) {
					Console.WriteLine ("Error: " + error.Message);
				}
			}
		}

		static void Line ()
		{
			Console.WriteLine ("-------------------");
		}
	}
}
