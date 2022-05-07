using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Securing;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.GroupClaims;
using MediatR;

namespace Business.Handlers.GroupsOperationClaims.Queries {
	public class GetGroupOperationClaimsQuery : IRequest<IDataResult<IQueryable<GroupOperationClaim>>> {
		public class GetGroupOperationClaimsQueryHandler : IRequestHandler<GetGroupOperationClaimsQuery, IDataResult<IQueryable<GroupOperationClaim>>> {
			private readonly IGroupClaimReadRepository groupClaimReadRepository;

			public GetGroupOperationClaimsQueryHandler(IGroupClaimReadRepository groupClaimReadRepository) {
				this.groupClaimReadRepository = groupClaimReadRepository;
			}

			[SecuredAspect(Priority = 1)]
			[CacheAspect(10)]
			public async Task<IDataResult<IQueryable<GroupOperationClaim>>> Handle(GetGroupOperationClaimsQuery request, CancellationToken cancellationToken) {
				var groupOperationClaim = this.groupClaimReadRepository.GetAll();
				return new SuccessDataResult<IQueryable<GroupOperationClaim>>(groupOperationClaim);
			}
		}
	}
}