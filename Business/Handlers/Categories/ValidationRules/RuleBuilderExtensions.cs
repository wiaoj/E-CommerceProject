using FluentValidation;

namespace Business.Handlers.Categories.ValidationRules {
	internal static class RuleBuilderExtensions {
		public static IRuleBuilder<T, Guid> Id<T>(this IRuleBuilder<T, Guid> ruleBuilder) {
			var options = ruleBuilder
				.NotNull()
				.NotEmpty();
			return options;
		}

		public static IRuleBuilder<T, String> Name<T>(this IRuleBuilder<T, String> ruleBuilder) {
			return Name<T>(ruleBuilder, 4, Byte.MaxValue);
		}
		public static IRuleBuilder<T, String> Name<T>(this IRuleBuilder<T, String> ruleBuilder, Byte minimumLength, Byte maximumLength) {
			var options = ruleBuilder
				.NotNull()
				.NotEmpty()
				.MinimumLength(minimumLength)
				.MaximumLength(maximumLength);
			return options;
		}
	}
}