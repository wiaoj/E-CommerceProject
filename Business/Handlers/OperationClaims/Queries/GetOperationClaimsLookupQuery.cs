using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Securing;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Entities.Dtos;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.OperationClaims;
using MediatR;

namespace Business.Handlers.OperationClaims.Queries {
	public class GetOperationClaimsLookupQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>> {
		public class GetOperationClaimsLookupQueryHandler : IRequestHandler<GetOperationClaimsLookupQuery, IDataResult<IEnumerable<SelectionItem>>> {
			private readonly IOperationClaimReadRepository operationClaimReadRepository;

			public GetOperationClaimsLookupQueryHandler(IOperationClaimReadRepository operationClaimReadRepository) {
				this.operationClaimReadRepository = operationClaimReadRepository;
			}

			[SecuredAspect(Priority = 1)]
			public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetOperationClaimsLookupQuery request, CancellationToken cancellationToken) {
				var list = this.operationClaimReadRepository.GetAll();

				var item = list.Select(x => new SelectionItem() {
					Id = x.Id,
					Label = x.Alias ?? x.Name
				});
				return new SuccessDataResult<IEnumerable<SelectionItem>>(item);
			}
		}
	}
}