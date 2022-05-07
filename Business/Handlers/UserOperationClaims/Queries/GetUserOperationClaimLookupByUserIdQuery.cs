using Core.Aspects.Autofac.Securing;
using Core.Entities.Dtos;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.UserOperationClaims;
using MediatR;

namespace Business.Handlers.UserOperationClaims.Queries {
	public class GetUserOperationClaimLookupByUserIdQuery : IRequest<IDataResult<IQueryable<SelectionItem>>> {
		public Guid UserId { get; set; }
		public class GetUserOperationClaimLookupByUserIdQueryHandler : IRequestHandler<GetUserOperationClaimLookupByUserIdQuery, IDataResult<IQueryable<SelectionItem>>> {
			private readonly IUserOperationClaimReadRepository userOperationClaimReadRepository;
			public GetUserOperationClaimLookupByUserIdQueryHandler(IUserOperationClaimReadRepository userClaimRepository) {
				this.userOperationClaimReadRepository = userClaimRepository;
			}

			[SecuredAspect(Priority = 1)]
			public async Task<IDataResult<IQueryable<SelectionItem>>> Handle(GetUserOperationClaimLookupByUserIdQuery request, CancellationToken cancellationToken) {
				var data = await this.userOperationClaimReadRepository.GetUserOperationClaimSelectedList(request.UserId);
				return new SuccessDataResult<IQueryable<SelectionItem>>(data);
			}
		}
	}
}