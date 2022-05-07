using FluentValidation;

namespace Core.CrossCuttingConcerns.Validation {
	public static class ValidationTool {
		public static void Validate(IValidator? validator, Object entity) {
			ValidationContext<Object> context = new(entity);

			var result = validator?.Validate(context);

			if(result is not null && result.IsValid is false)
				throw new ValidationException(result.Errors);
		}
	}
}