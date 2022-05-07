using Business.Handlers.ProductImages.Commands;
using FluentValidation;

namespace Business.Handlers.ProductImages.ValidationRules {
	public class CreateProductImageValidator : AbstractValidator<CreateProductImageCommand> {
		public CreateProductImageValidator() {
			this.RuleFor(x => x.ProductId).ProductId();

			this.RuleFor(x => x.Image).Image();
		}
	}
}