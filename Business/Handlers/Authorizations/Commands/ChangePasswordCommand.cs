using Business.Constants.Messages;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Securing;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Error;
using Core.Utilities.Results.Concrete.Success;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract.Repositories.Users;
using MediatR;

namespace Business.Handlers.Authorizations.Commands {
	public class ChangePasswordCommand : IRequest<IResult> {
		public Guid Id { get; set; }
#pragma warning disable CS8618 // Non-nullable property 'Password' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public String Password { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Password' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

		public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, IResult> {
			private readonly IUserWriteRepository userWriteRepository;
			private readonly IUserReadRepository userReadRepository;

			public ChangePasswordCommandHandler(IUserWriteRepository userWriteRepository,
				IUserReadRepository userReadRepository) {
				this.userWriteRepository = userWriteRepository;
				this.userReadRepository = userReadRepository;
			}

			[SecuredAspect(Priority = 1)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken) {
				var isThereAnyUser = await this.userReadRepository.GetSingleAsync(user => user.Id.Equals(request.Id));

				if(isThereAnyUser is null) {
					return new ErrorResult(Messages.UserNotFound);
				}

				HashingHelper.CreatePasswordHash(request.Password, out Byte[] passwordSalt, out Byte[] passwordHash);

				isThereAnyUser.PasswordHash = passwordHash;
				isThereAnyUser.PasswordSalt = passwordSalt;

				this.userWriteRepository.Update(isThereAnyUser);
				await this.userWriteRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Updated);
			}
		}
	}
}