using System;

namespace EasyFlow.Core
{
	public interface IAuthManager
	{
		bool UserHasApprovedStateTransition (Guid workflowId, string currentState, string newState, string username);

		void ApproveStateTransition(Guid workflowId, string currentState, string newState, string username);

		bool UserHasPermission(string username, string permission);

		bool PermissionIsValid(string permission);
	}
}

