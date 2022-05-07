using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Securing;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Entities.Dtos;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.GroupClaims;
using MediatR;

namespace Business.Handlers.GroupsOperationClaims.Queries {
	public class GetGroupOperationClaimsLookupByGroupIdQuery : IRequest<IDataResult<IQueryable<SelectionItem>>> {
		public Guid GroupId { get; set; }
		public class GetGroupClaimsLookupByGroupIdQueryHandler : IRequestHandler<GetGroupOperationClaimsLookupByGroupIdQuery,
			IDataResult<IQueryable<SelectionItem>>> {
			private readonly IGroupClaimReadRepository groupClaimReadRepository;

			public GetGroupClaimsLookupByGroupIdQueryHandler(IGroupClaimReadRepository groupClaimReadRepository) {
				this.groupClaimReadRepository = groupClaimReadRepository;
			}

			[SecuredAspect(Priority = 1)]
			public async Task<IDataResult<IQueryable<SelectionItem>>> Handle(
				GetGroupOperationClaimsLookupByGroupIdQuery request, CancellationToken cancellationToken) {
				var data = await this.groupClaimReadRepository.GetGroupClaimsSelectedList(request.GroupId);
				return new SuccessDataResult<IQueryable<SelectionItem>>(data);
			}
		}
	}
}