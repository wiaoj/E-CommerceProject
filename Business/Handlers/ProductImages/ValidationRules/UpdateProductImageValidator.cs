using Business.Handlers.ProductImages.Commands;
using FluentValidation;

namespace Business.Handlers.ProductImages.ValidationRules {
	public class UpdateProductImageValidator : AbstractValidator<UpdateProductImageCommand> {
		public UpdateProductImageValidator() {
			this.RuleFor(x => x.Id).Id();

			this.RuleFor(x => x.Image).Image();
		}
	}
}