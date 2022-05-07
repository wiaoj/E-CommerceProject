using Core.Aspects.Autofac.Securing;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.GroupClaims;
using MediatR;

namespace Business.Handlers.GroupsOperationClaims.Queries {
	public class GetGroupOperationClaimQuery : IRequest<IDataResult<IQueryable<GroupOperationClaim>>> {
		public Guid Id { get; set; }

		public class GetGroupOperationClaimQueryHandler : IRequestHandler<GetGroupOperationClaimQuery, IDataResult<IQueryable<GroupOperationClaim>>> {
			private readonly IGroupClaimReadRepository groupClaimReadRepository;
			public GetGroupOperationClaimQueryHandler(IGroupClaimReadRepository groupClaimReadRepository) {
				this.groupClaimReadRepository = groupClaimReadRepository;
			}

			[SecuredAspect(Priority = 1)]
			public async Task<IDataResult<IQueryable<GroupOperationClaim>>> Handle(GetGroupOperationClaimQuery request, CancellationToken cancellationToken) {
				var groupClaimList = this.groupClaimReadRepository.GetWhere(x => x.GroupId.Equals(request.Id));
				return new SuccessDataResult<IQueryable<GroupOperationClaim>>(groupClaimList);
			}
		}
	}
}