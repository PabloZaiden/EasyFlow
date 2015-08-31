using System;
using System.Collections.Generic;

namespace EasyFlow.Core
{
	public interface IRule
	{
		/// <summary>
		/// Validate the specified workflow, transition and parameters.
		/// </summary>
		/// <param name="workflow">Workflow.</param>
		/// <param name="transition">Transition.</param>
		/// <param name="parameters">Parameters.</param>
		bool IsValid (Workflow workflow, Transition transition, params string[] parameters);

		/// <summary>
		/// Gets the validation errors.
		/// </summary>
		/// <returns>The validation errors.</returns>
		/// <param name="workflow">Workflow.</param>
		/// <param name="transition">Transition.</param>
		/// <param name="parameters">Parameters.</param>
		List<ValidationError> GetValidationErrors (Workflow workflow, Transition transition, params string[] parameters);
	}
}

