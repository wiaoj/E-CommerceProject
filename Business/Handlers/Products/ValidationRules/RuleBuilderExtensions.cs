using FluentValidation;

namespace Business.Handlers.Products.ValidationRules {
	internal static class RuleBuilderExtensions {
		public static IRuleBuilder<T, Guid> Id<T>(this IRuleBuilder<T, Guid> ruleBuilder) {
			var options = ruleBuilder
				.NotNull()
				.NotEmpty();
			return options;
		}

		public static IRuleBuilder<T, String> Name<T>(this IRuleBuilder<T, String> ruleBuilder) {
			return Name<T>(ruleBuilder, 8, 512);
		}
		public static IRuleBuilder<T, String> Name<T>(this IRuleBuilder<T, String> ruleBuilder, Byte minimumLength, Int16 maximumLength) {
			var options = ruleBuilder
				.NotNull()
				.NotEmpty()
				.MinimumLength(minimumLength)
				.MaximumLength(maximumLength);
			return options;
		}

		public static IRuleBuilder<T, Decimal> UnitPrice<T>(this IRuleBuilder<T, Decimal> ruleBuilder) {
			var options = ruleBuilder
				.NotNull()
				.NotEmpty()
				.GreaterThanOrEqualTo(0.01M)
				.LessThanOrEqualTo(Decimal.MaxValue);
			return options;
		}

		public static IRuleBuilder<T, Int16> UnitInStock<T>(this IRuleBuilder<T, Int16> ruleBuilder) {
			var options = ruleBuilder
				.NotNull()
				.NotEmpty()
				.GreaterThan(default(Int16))
				.LessThanOrEqualTo(Int16.MaxValue);
			return options;
		}

		public static IRuleBuilder<T, String> Description<T>(this IRuleBuilder<T, String> ruleBuilder) {
			return Description<T>(ruleBuilder, 16, 10000);
		}
		public static IRuleBuilder<T, String> Description<T>(this IRuleBuilder<T, String> ruleBuilder, Int16 minimumLength, Int16 maximumLength) {
			var options = ruleBuilder
				.NotNull()
				.NotEmpty()
				.MinimumLength(minimumLength)
				.MaximumLength(maximumLength);
			return options;
		}

	}
}
