using Business.Constants.Messages;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Securing;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.GroupClaims;
using MediatR;

namespace Business.Handlers.GroupsOperationClaims.Commands {
	public class UpdateGroupOperationClaimCommand : IRequest<IResult> {
		public Guid Id { get; set; }
		public Guid GroupId { get; set; }
		public Guid[] ClaimIds { get; set; }

		public class UpdateGroupOperationClaimCommandHandler : IRequestHandler<UpdateGroupOperationClaimCommand, IResult> {
			private readonly IGroupClaimWriteRepository groupClaimWriteRepository;
			public UpdateGroupOperationClaimCommandHandler(IGroupClaimWriteRepository groupClaimWriteRepository) {
				this.groupClaimWriteRepository = groupClaimWriteRepository;
			}

			[SecuredAspect(Priority = 1)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(UpdateGroupOperationClaimCommand request, CancellationToken cancellationToken) {
				var list = request.ClaimIds.Select(x =>
				new GroupOperationClaim() {
					OperationClaimId = x,
					GroupId = request.GroupId
				});

				await this.groupClaimWriteRepository.BulkInsert(request.GroupId, list);
				await this.groupClaimWriteRepository.SaveChangesAsync();

				return new SuccessResult(Messages.Updated);
			}
		}
	}
}