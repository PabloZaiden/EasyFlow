using System;

namespace EasyFlow.Core
{
	public class ValidationError
	{
		public ValidationError (String message)
		{
			Message = message;
		}

		public String Message {
			get;
		}
	}
}

