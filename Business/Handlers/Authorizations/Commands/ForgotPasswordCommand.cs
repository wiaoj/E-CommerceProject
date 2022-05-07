using Business.Constants.Messages;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Securing;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Error;
using Core.Utilities.Results.Concrete.Success;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Toolkit;
using DataAccess.Abstract.Repositories.Users;
using MediatR;

namespace Business.Handlers.Authorizations.Commands {
	public class ForgotPasswordCommand : IRequest<IResult> {
#pragma warning disable CS8618 // Non-nullable property 'EmailOrPhoneNumber' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public String EmailOrPhoneNumber { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'EmailOrPhoneNumber' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

		public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, IResult> {
			private readonly IUserWriteRepository userWriteRepository;
			private readonly IUserReadRepository userReadRepository;

			public ForgotPasswordCommandHandler(IUserWriteRepository userWriteRepository,
				IUserReadRepository userReadRepository) {
				this.userWriteRepository = userWriteRepository;
				this.userReadRepository = userReadRepository;
			}

			[SecuredAspect(Priority = 1)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken) {
				var isThereAnyUser = await this.userReadRepository.GetSingleAsync(u => (u.Email.Equals(request.EmailOrPhoneNumber) || u.PhoneNumber.Equals(request.EmailOrPhoneNumber)) && u.Status);

				if(isThereAnyUser is null) {
					return new ErrorResult(Messages.UserNotFound);
				}

				var generatedPassword = RandomGenerator.RandomPasswordGenerator();
				HashingHelper.CreatePasswordHash(generatedPassword, out Byte[] passwordSalt, out Byte[] passwordHash);

				isThereAnyUser.PasswordSalt = passwordSalt;
				isThereAnyUser.PasswordHash = passwordHash;
				//user.UpdateContactDate = DateTime.Now;

				this.userWriteRepository.Update(isThereAnyUser);
				await this.userWriteRepository.SaveChangesAsync();
				return new SuccessResult($"{Messages.NewPassword}: {generatedPassword}");
			}
		}
	}
}