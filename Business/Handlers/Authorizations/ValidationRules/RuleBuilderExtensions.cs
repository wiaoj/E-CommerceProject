using FluentValidation;

namespace Business.Handlers.Authorizations.ValidationRules {
	internal static class RuleBuilderExtensions {
		/// <summary>
		/// minimum Password lenght => 8
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="ruleBuilder"></param>
		/// <returns></returns>
		public static IRuleBuilder<T, String> Password<T>(this IRuleBuilder<T, String> ruleBuilder) {
			return Password<T>(ruleBuilder, 8, Byte.MaxValue);
		}
		public static IRuleBuilder<T, String> Password<T>(this IRuleBuilder<T, String> ruleBuilder, Byte minimumLength, Byte maximumLength) {
			var options = ruleBuilder
				.NotNull()
				.NotEmpty()
				.MinimumLength(minimumLength)
				.MaximumLength(maximumLength)
				.Matches("[A-Z]")
				.Matches("[a-z]")
				.Matches("[0-9]")
				.Matches("[^a-zA-Z0-9]");
			return options;
		}
	}
}