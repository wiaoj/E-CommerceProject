using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Securing;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.OperationClaims;
using MediatR;

namespace Business.Handlers.OperationClaims.Queries {
	public class GetOperationClaimsQuery : IRequest<IDataResult<IEnumerable<OperationClaim>>> {
		public class GetOperationClaimsQueryHandler : IRequestHandler<GetOperationClaimsQuery, IDataResult<IEnumerable<OperationClaim>>> {
			private readonly IOperationClaimReadRepository operationClaimReadRepository;

			public GetOperationClaimsQueryHandler(IOperationClaimReadRepository operationClaimReadRepository) {
				this.operationClaimReadRepository = operationClaimReadRepository;
			}

			[SecuredAspect(Priority = 1)]
			[CacheAspect(10)]
			public async Task<IDataResult<IEnumerable<OperationClaim>>> Handle(GetOperationClaimsQuery request, CancellationToken cancellationToken) {
				return new SuccessDataResult<IEnumerable<OperationClaim>>(this.operationClaimReadRepository.GetAll());
			}
		}
	}
}