using Business.Handlers.Products.Commands;
using FluentValidation;

namespace Business.Handlers.Products.ValidationRules {
	internal class CreateProductValidator : AbstractValidator<CreateProductCommand> {
		public CreateProductValidator() {
			this.RuleFor(x => x.Name).Name();

			this.RuleFor(x => x.UnitPrice).UnitPrice();

			this.RuleFor(x => x.UnitInStock).UnitInStock();

			this.RuleFor(x => x.Description).Description();
		}
	}
}