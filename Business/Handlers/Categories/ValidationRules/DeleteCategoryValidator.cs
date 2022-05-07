using Business.Handlers.Categories.Commands;
using FluentValidation;

namespace Business.Handlers.Categories.ValidationRules {
	internal class DeleteCategoryValidator : AbstractValidator<DeleteCategoryCommand> {
		public DeleteCategoryValidator() {
			this.RuleFor(x => x.Id).Id();
		}
	}
}