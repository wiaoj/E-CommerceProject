using Business.Constants.Messages;
using Core.Aspects.Autofac.Securing;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Error;
using Core.Utilities.Results.Concrete.Success;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract.Repositories.Users;
using MediatR;

namespace Business.Handlers.Users.Commands {
	public class UserChangePasswordCommand : IRequest<IResult> {
		public Guid Id { get; set; }
		public String Password { get; set; }

		public class UserChangePasswordCommandHandler : IRequestHandler<UserChangePasswordCommand, IResult> {
			private readonly IUserWriteRepository userWriteRepository;
			private readonly IUserReadRepository userReadRepository;
			private readonly IMediator mediator;

			public UserChangePasswordCommandHandler(IUserWriteRepository userWriteRepository, IMediator mediator) {
				this.userWriteRepository = userWriteRepository;
				this.mediator = mediator;
			}

			[SecuredAspect(Priority = 1)]
			public async Task<IResult> Handle(UserChangePasswordCommand request, CancellationToken cancellationToken) {
				var isThereAnyUser = await this.userReadRepository.GetSingleAsync(u => u.Id.Equals(request.Id));
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