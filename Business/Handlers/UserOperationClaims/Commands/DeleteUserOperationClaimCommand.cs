using Business.Constants.Messages;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Securing;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.UserOperationClaims;
using MediatR;

namespace Business.Handlers.UserOperationClaims.Commands {
	public class DeleteUserOperationClaimCommand : IRequest<IResult> {
		public Guid UserId { get; set; }

		public class DeleteUserOperationClaimCommandHandler : IRequestHandler<DeleteUserOperationClaimCommand, IResult> {
			private readonly IUserOperationClaimWriteRepository userOperationClaimWriteRepository;
			private readonly IUserOperationClaimReadRepository userOperationClaimReadRepository;

			public DeleteUserOperationClaimCommandHandler(IUserOperationClaimWriteRepository userOperationClaimWriteRepository,
				IUserOperationClaimReadRepository userOperationClaimReadRepository) {
				this.userOperationClaimWriteRepository = userOperationClaimWriteRepository;
				this.userOperationClaimReadRepository = userOperationClaimReadRepository;
			}

			[SecuredAspect(Priority = 1)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(DeleteUserOperationClaimCommand request, CancellationToken cancellationToken) {
				var entityToDelete = this.userOperationClaimReadRepository.GetWhere(x => x.UserId.Equals(request.UserId));

				this.userOperationClaimWriteRepository.RemoveRange(entityToDelete);
				await this.userOperationClaimWriteRepository.SaveChangesAsync();

				return new SuccessResult(Messages.Deleted);
			}
		}
	}
}