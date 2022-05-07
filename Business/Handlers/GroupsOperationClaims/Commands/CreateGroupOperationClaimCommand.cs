using Business.Constants.Messages;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Securing;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Error;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.OperationClaims;
using MediatR;

namespace Business.Handlers.GroupsOperationClaims.Commands {
	public class CreateGroupOperationClaimCommand : IRequest<IResult> {
		public String ClaimName { get; set; }

		public class CreateGroupOperationClaimCommandHandler : IRequestHandler<CreateGroupOperationClaimCommand, IResult> {
			private readonly IOperationClaimWriteRepository operationClaimWriteRepository;
			private readonly IOperationClaimReadRepository operationClaimReadRepository;
			public CreateGroupOperationClaimCommandHandler(IOperationClaimWriteRepository operationClaimWriteRepository,
				IOperationClaimReadRepository operationClaimReadRepository) {
				this.operationClaimWriteRepository = operationClaimWriteRepository;
				this.operationClaimReadRepository = operationClaimReadRepository;
			}

			[SecuredAspect(Priority = 1)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(CreateGroupOperationClaimCommand request, CancellationToken cancellationToken) {
				if(this.IsClaimExists(request.ClaimName))
					return new ErrorResult(Messages.OperationClaimExists);

				var operationClaim = new OperationClaim {
					Name = request.ClaimName
				};

				await this.operationClaimWriteRepository.AddAsync(operationClaim);
				await this.operationClaimWriteRepository.SaveChangesAsync();

				return new SuccessResult(Messages.Added);
			}

			private Boolean IsClaimExists(String claimName) {
				return this.operationClaimReadRepository.GetWhere(x => x.Name.Equals(claimName)) is not null;
			}
		}
	}
}