using System;
using System.Collections.Generic;

namespace EasyFlow.Core
{
	public class ValidationException : Exception
	{
		public ValidationException (List<ValidationError> errors)
			: base ("There were some validation errors")
		{
		}

		public List<ValidationError> Errors {
			get;
			set;
		}
	}
}

