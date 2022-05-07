using Business.Constants.Messages;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Securing;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract;
using DataAccess.Abstract.Repositories.Users;
using MediatR;

namespace Business.Handlers.Users.Commands {
	public class UpdateUserCommand : IRequest<IResult> {
		public Guid UserId { get; set; }
		public String Email { get; set; }
		public String FirstName { get; set; }
		public String LastName { get; set; }
		public String PhoneNumber { get; set; }

		public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, IResult> {
			private readonly IUserWriteRepository userWriteRepository;
			private readonly IUserReadRepository userReadRepository;

			public UpdateUserCommandHandler(IUserReadRepository userReadRepository, 
				IUserWriteRepository userWriteRepository) {
				this.userReadRepository = userReadRepository;
				this.userWriteRepository = userWriteRepository;
			}

			[SecuredAspect(Priority = 1)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken) {
				var isThereAnyUser = await this.userReadRepository.GetSingleAsync(u => u.Id.Equals(request.UserId));

				isThereAnyUser.FirstName = request.FirstName;
				isThereAnyUser.LastName = request.LastName;
				isThereAnyUser.Email = request.Email;
				isThereAnyUser.PhoneNumber = request.PhoneNumber;
				//isThereAnyUser.Address = request.Address;

				this.userWriteRepository.Update(isThereAnyUser);
				await this.userWriteRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Updated);
			}
		}
	}
}