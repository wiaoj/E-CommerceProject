using Business.Constants.Messages;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Securing;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.GroupClaims;
using MediatR;

namespace Business.Handlers.GroupsOperationClaims.Commands {
	public class DeleteGroupOperationClaimCommand : IRequest<IResult> {
		public Guid Id { get; set; }

		public class DeleteGroupOperationClaimCommandHandler : IRequestHandler<DeleteGroupOperationClaimCommand, IResult> {
			private readonly IGroupClaimWriteRepository groupClaimWriteRepository;
			public DeleteGroupOperationClaimCommandHandler(IGroupClaimWriteRepository groupClaimWriteRepository) {
				this.groupClaimWriteRepository = groupClaimWriteRepository;
			}

			[SecuredAspect(Priority = 1)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(DeleteGroupOperationClaimCommand request, CancellationToken cancellationToken) {
				await this.groupClaimWriteRepository.RemoveAsync(request.Id);
				await this.groupClaimWriteRepository.SaveChangesAsync();

				return new SuccessResult(Messages.Deleted);
			}
		}
	}
}