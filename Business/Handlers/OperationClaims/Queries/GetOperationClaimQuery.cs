using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Securing;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.OperationClaims;
using MediatR;

namespace Business.Handlers.OperationClaims.Queries {
	public class GetOperationClaimQuery : IRequest<IDataResult<OperationClaim>> {
		public Guid Id { get; set; }

		public class GetOperationClaimQueryHandler : IRequestHandler<GetOperationClaimQuery, IDataResult<OperationClaim>> {
			private readonly IOperationClaimReadRepository operationClaimReadRepository;

			public GetOperationClaimQueryHandler(IOperationClaimReadRepository operationClaimReadRepository) {
				this.operationClaimReadRepository = operationClaimReadRepository;
			}

			[SecuredAspect(Priority = 1)]
			public async Task<IDataResult<OperationClaim>> Handle(GetOperationClaimQuery request, CancellationToken cancellationToken) {
				return new SuccessDataResult<OperationClaim>(await this.operationClaimReadRepository.GetSingleAsync(x => x.Id.Equals(request.Id)));
			}
		}
	}
}