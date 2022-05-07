using Business.Handlers.Categories.Commands;
using FluentValidation;

namespace Business.Handlers.Categories.ValidationRules {
	internal class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand> {
		public CreateCategoryValidator() {
			this.RuleFor(x => x.Name).Name();
		}
	}
}