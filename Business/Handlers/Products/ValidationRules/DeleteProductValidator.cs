using Business.Handlers.Products.Commands;
using FluentValidation;

namespace Business.Handlers.Products.ValidationRules {
	internal class DeleteProductValidator : AbstractValidator<DeleteProductCommand> {
		public DeleteProductValidator() {
			this.RuleFor(x => x.Id).Id();
		}
	}
}