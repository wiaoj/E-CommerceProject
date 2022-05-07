using Business.Constants.Messages;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Securing;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.OperationClaims;
using MediatR;

namespace Business.Handlers.OperationClaims.Commands {
	public class DeleteOperationClaimCommand : IRequest<IResult> {
		public Guid Id { get; set; }

		public class DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, IResult> {
			private readonly IOperationClaimWriteRepository operationClaimWriteRepository;

			public DeleteOperationClaimCommandHandler(IOperationClaimWriteRepository operationClaimWriteRepository) {
				this.operationClaimWriteRepository = operationClaimWriteRepository;
			}

			[SecuredAspect(Priority = 1)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken) {
				await this.operationClaimWriteRepository.RemoveAsync(request.Id);
				await this.operationClaimWriteRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Deleted);
			}
		}
	}
}