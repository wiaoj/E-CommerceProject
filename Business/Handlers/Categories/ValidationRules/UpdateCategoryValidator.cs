using Business.Handlers.Categories.Commands;
using FluentValidation;

namespace Business.Handlers.Categories.ValidationRules {
	internal class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand> {
		public UpdateCategoryValidator() {
			this.RuleFor(x => x.Id).Id();
			this.RuleFor(x => x.Name).Name();
		}
	}
}