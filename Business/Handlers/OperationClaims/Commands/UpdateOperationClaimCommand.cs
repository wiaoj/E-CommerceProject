using Business.Constants.Messages;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Securing;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract;
using DataAccess.Abstract.Repositories.OperationClaims;
using MediatR;

namespace Business.Handlers.OperationClaims.Commands {
	public class UpdateOperationClaimCommand : IRequest<IResult> {
		public Guid Id { get; set; }
		public String Alias { get; set; }
		public String Description { get; set; }

		public class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, IResult> {
			private readonly IOperationClaimWriteRepository operationClaimWriteRepository;
			private readonly IOperationClaimReadRepository operationClaimReadRepository;

			public UpdateOperationClaimCommandHandler(IOperationClaimWriteRepository operationClaimRepository, 
				IOperationClaimReadRepository operationClaimReadRepository) {
				this.operationClaimWriteRepository = operationClaimRepository;
				this.operationClaimReadRepository = operationClaimReadRepository;
			}

			[SecuredAspect(Priority = 1)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken) {
				var isOperationClaimExists = await this.operationClaimReadRepository.GetSingleAsync(u => u.Id.Equals(request.Id));
				isOperationClaimExists.Alias = request.Alias;
				isOperationClaimExists.Description = request.Description;

				this.operationClaimWriteRepository.Update(isOperationClaimExists);
				await this.operationClaimWriteRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Updated);
			}
		}
	}
}