using System;

namespace EasyFlow.Core
{
	public class UserValidationError : ValidationError
	{
		public UserValidationError (String username) 
			: base("The user " + username + " is missing")
		{
			Username = username;
		}

		public String Username {
			get;
			private set;
		}
	}
}

