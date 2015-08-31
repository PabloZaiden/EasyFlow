using System;

namespace EasyFlow.Core
{
	public static class Logger
	{
		public static void Log(LogLevel level, String message) {
			Console.WriteLine ();
			Console.WriteLine ("LEVEL: " + level.ToString());
			Console.WriteLine (message);
			Console.WriteLine ();
		}

		public static void Log(LogLevel level, String pattern, params object[] parameters) {
			Log (level, String.Format (pattern, parameters));
		}

		public static void Log(Exception ex) {
			Log (LogLevel.Error, ex.ToString ());
		}
	}
}

