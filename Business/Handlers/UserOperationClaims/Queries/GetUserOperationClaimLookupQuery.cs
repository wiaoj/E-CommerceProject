using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Securing;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.UserOperationClaims;
using MediatR;

namespace Business.Handlers.UserOperationClaims.Queries {
	public class GetUserOperationClaimLookupQuery : IRequest<IDataResult<IQueryable<UserOperationClaim>>> {
		public Guid UserId { get; set; }

		public class GetUserOperationClaimQueryHandler : IRequestHandler<GetUserOperationClaimLookupQuery, IDataResult<IQueryable<UserOperationClaim>>> {
			private readonly IUserOperationClaimReadRepository userOperationClaimReadRepository;

			public GetUserOperationClaimQueryHandler(IUserOperationClaimReadRepository userOperationClaimReadRepository) {
				this.userOperationClaimReadRepository = userOperationClaimReadRepository;
			}

			[SecuredAspect(Priority = 1)]
			[CacheAspect(10)]
			public async Task<IDataResult<IQueryable<UserOperationClaim>>> Handle(GetUserOperationClaimLookupQuery request, CancellationToken cancellationToken) {
				var userClaims = this.userOperationClaimReadRepository.GetWhere(x => x.UserId.Equals(request.UserId));

				return new SuccessDataResult<IQueryable<UserOperationClaim>>(userClaims);
			}
		}
	}
}