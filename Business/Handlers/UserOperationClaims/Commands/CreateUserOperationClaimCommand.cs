using Business.Constants.Messages;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Securing;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.UserOperationClaims;
using MediatR;

namespace Business.Handlers.UserOperationClaims.Commands {
	/// <summary> Internal
	/// For Internal Use Only,
	/// Registers All Existing Operation Claims To Given User
	/// </summary>
	public class CreateUserOperationClaimCommand : IRequest<IResult> {
		public Guid UserId { get; set; }
		public IEnumerable<OperationClaim> OperationClaims { get; set; }

		public class CreateUserOperationClaimCommandHandler : IRequestHandler<CreateUserOperationClaimCommand, IResult> {
			private readonly IUserOperationClaimWriteRepository userOperationClaimWriteRepository;
			private readonly IUserOperationClaimReadRepository userOperationClaimReadRepository;

			public CreateUserOperationClaimCommandHandler(IUserOperationClaimWriteRepository userOperationClaimWriteRepository,
				IUserOperationClaimReadRepository userOperationClaimReadRepository) {
				this.userOperationClaimWriteRepository = userOperationClaimWriteRepository;
				this.userOperationClaimReadRepository = userOperationClaimReadRepository;
			}

			[SecuredAspect(Priority = 1)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(CreateUserOperationClaimCommand request, CancellationToken cancellationToken) {
				foreach(var claim in request.OperationClaims) {
					if(await this.DoesClaimExistsForUser(new UserOperationClaim {
						Id = claim.Id,
						UserId = request.UserId
					})) {
						continue;
					}

					await this.userOperationClaimWriteRepository.AddAsync(new UserOperationClaim {
						OperationClaimId = claim.Id,
						UserId = request.UserId
					});
				}
				await this.userOperationClaimWriteRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Added);
			}

			private async Task<Boolean> DoesClaimExistsForUser(UserOperationClaim userClaim) {
				return (await this.userOperationClaimReadRepository.GetSingleAsync(x => x.UserId.Equals(userClaim.UserId) && x.OperationClaimId.Equals(userClaim.OperationClaimId))) is { };
			}
		}
	}
}