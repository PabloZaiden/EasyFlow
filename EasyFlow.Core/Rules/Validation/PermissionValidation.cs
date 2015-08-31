using System;

namespace EasyFlow.Core
{
	public class PermissionValidation : ValidationError
	{
		public PermissionValidation (String user, String permission)
			: base("User " + user + " has no permission for " + permission)
		{
			User = user;
			Permission = permission;
		}

		public String User {
			get;
			set;
		}

		public String Permission {
			get;
			set;
		}
	}
}

