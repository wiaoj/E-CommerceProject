using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Business.Handlers.ProductImages.ValidationRules {
	internal static class RuleBuilderExtension {
		public static IRuleBuilder<T, Guid> Id<T>(this IRuleBuilder<T, Guid> ruleBuilder) {
			var options = ruleBuilder
				.NotNull()
				.NotEmpty();
			return options;
		}
		public static IRuleBuilder<T, Guid> ProductId<T>(this IRuleBuilder<T, Guid> ruleBuilder) {
			var options = ruleBuilder
				.NotNull()
				.NotEmpty();
			return options;
		}
		public static IRuleBuilder<T, IFormFile> Image<T>(this IRuleBuilder<T, IFormFile> ruleBuilder) {
			var options = ruleBuilder
				.NotNull()
				.NotEmpty()
				.Must(x => x.ContentType.Contains("image/"))
				.Must(x => x.Length <= 8_388_608).WithMessage("Dosya boyutu 8mb üstü olamaz");
			return options;
		}
	}
}