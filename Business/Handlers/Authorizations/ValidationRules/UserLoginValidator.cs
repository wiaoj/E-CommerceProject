using Business.Handlers.Authorizations.Queries;
using FluentValidation;

namespace Business.Handlers.Authorizations.ValidationRules {
	public class UserLoginValidator : AbstractValidator<UserLoginQuery> {
		public UserLoginValidator() {
			this.RuleFor(m => m.EmailOrPhoneNumber).NotEmpty();
			this.RuleFor(m => m.Password).NotEmpty();
		}
	}
}