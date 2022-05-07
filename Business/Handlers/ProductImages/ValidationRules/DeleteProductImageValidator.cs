using Business.Handlers.ProductImages.Commands;
using FluentValidation;

namespace Business.Handlers.ProductImages.ValidationRules {
	public class DeleteProductImageValidator : AbstractValidator<DeleteProductImageCommand> {
		public DeleteProductImageValidator() {
			this.RuleFor(x => x.Id).Id();
		}
	}
}