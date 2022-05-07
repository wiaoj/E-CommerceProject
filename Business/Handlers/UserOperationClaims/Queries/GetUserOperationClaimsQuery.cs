using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Securing;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.UserOperationClaims;
using MediatR;

namespace Business.Handlers.UserOperationClaims.Queries {
	public class GetUserOperationClaimsQuery : IRequest<IDataResult<IQueryable<UserOperationClaim>>> {
		public class GetUserOperationClaimsQueryHandler : IRequestHandler<GetUserOperationClaimsQuery, IDataResult<IQueryable<UserOperationClaim>>> {
			private readonly IUserOperationClaimReadRepository userOperationClaimReadRepository;

			public GetUserOperationClaimsQueryHandler(IUserOperationClaimReadRepository userOperationClaimRepository) {
				this.userOperationClaimReadRepository = userOperationClaimRepository;
			}

			[SecuredAspect(Priority = 1)]
			[CacheAspect(10)]
			public async Task<IDataResult<IQueryable<UserOperationClaim>>> Handle(GetUserOperationClaimsQuery request, CancellationToken cancellationToken) {
				return new SuccessDataResult<IQueryable<UserOperationClaim>>(this.userOperationClaimReadRepository.GetAll());
			}
		}
	}
}