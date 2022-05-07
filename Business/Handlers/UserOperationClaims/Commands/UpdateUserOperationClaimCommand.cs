using Business.Constants.Messages;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Securing;
using Core.CrossCuttingConcerns.Caching;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.UserOperationClaims;
using MediatR;

namespace Business.Handlers.UserOperationClaims.Commands {
	public class UpdateUserOperationClaimCommand : IRequest<IResult> {
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public Guid[] ClaimId { get; set; }

		public class UpdateUserOperationClaimCommandHandler : IRequestHandler<UpdateUserOperationClaimCommand, IResult> {
			private readonly IUserOperationClaimWriteRepository userOperationClaimWriteRepository;
			private readonly ICacheManager cacheManager;

			public UpdateUserOperationClaimCommandHandler(IUserOperationClaimWriteRepository userOperationClaimWriteRepository,
				ICacheManager cacheManager) {
				this.userOperationClaimWriteRepository = userOperationClaimWriteRepository;
				this.cacheManager = cacheManager;
			}

			[SecuredAspect(Priority = 1)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(UpdateUserOperationClaimCommand request, CancellationToken cancellationToken) {
				var userList = request.ClaimId.Select(x =>
				new UserOperationClaim() {
					Id = x,
					UserId = request.UserId
				});

				await this.userOperationClaimWriteRepository.BulkInsert(request.UserId, userList);
				await this.userOperationClaimWriteRepository.SaveChangesAsync();

				this.cacheManager.Remove($"{CacheKeys.UserIdForClaim}={request.UserId}");

				return new SuccessResult(Messages.Updated);
			}
		}
	}
}