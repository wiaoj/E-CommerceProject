using Business.Handlers.Authorizations.Queries;
using FluentValidation;

namespace Business.Handlers.Authorizations.ValidationRules {
	public class UserRegisterValidator : AbstractValidator<UserRegisterQuery> {
		public UserRegisterValidator() {
			this.RuleFor(p => p.Password).Password();
		}
	}
}