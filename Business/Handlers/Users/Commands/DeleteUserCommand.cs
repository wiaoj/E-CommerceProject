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
	public class DeleteUserCommand : IRequest<IResult> {
		public Guid Id { get; set; }

		public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, IResult> {
			private readonly IUserReadRepository userReadRepository;
			private readonly IUserWriteRepository userWriteRepository;

			public DeleteUserCommandHandler(IUserReadRepository userReadRepository, 
				IUserWriteRepository userWriteRepository) {
				this.userReadRepository = userReadRepository;
				this.userWriteRepository = userWriteRepository;
			}

			[SecuredAspect(Priority = 1)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken) { 
				var userToDelete = await this.userReadRepository.GetSingleAsync(user => user.Id.Equals(request.Id));

				userToDelete.Status = false;
				this.userWriteRepository.Update(userToDelete);
				await this.userWriteRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Deleted);
			}
		}
	}
}