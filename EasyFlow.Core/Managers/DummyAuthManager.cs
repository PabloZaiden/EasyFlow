using System;
using System.Collections.Generic;

namespace EasyFlow.Core
{
	public class DummyAuthManager : IAuthManager
	{
		private DummyAuthManager() {
		}

		private static DummyAuthManager _instance = new DummyAuthManager();
		public static DummyAuthManager Instance {
			get {
				return _instance;
			}
		}

		List<KeyValuePair<String, String>> _permissions = new List<KeyValuePair<string, string>>();
		List<String> _approvals = new List<string>(); 


		public void AddPermission(string username, string permission) {
			_permissions.Add (new KeyValuePair<String, String> (username, permission));
		}

		#region IAuthManager implementation

		public bool UserHasApprovedStateTransition (Guid workflowId, string currentState, string newState, string username)
		{
			return _approvals.Contains (workflowId.ToString () + currentState + newState + username);
		}

		public void ApproveStateTransition (Guid workflowId, string currentState, string newState, string username)
		{
			_approvals.Add (workflowId.ToString () + currentState + newState + username);
		}


		public bool UserHasPermission (string username, string permission)
		{
			if (permission == null) {
				return true;
			}

			return _permissions.Contains (new KeyValuePair<String, String> (username, permission));
		}

		public bool PermissionIsValid (string permission)
		{
			return true;
		}
		#endregion
	}
}

