using AutoMapper;
using Business.Constants.Messages;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Securing;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.UserGroups;
using MediatR;

namespace Business.Handlers.UserGroups.Commands {
	public class CreateUserGroupOperationClaimsCommand : IRequest<IResult> {
		public Guid UserId { get; set; }
#pragma warning disable CS8618 // Non-nullable property 'UserGroups' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public IEnumerable<UserGroup> UserGroups { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'UserGroups' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

		public class CreateGroupOperationClaimsCommandHandler : IRequestHandler<CreateUserGroupOperationClaimsCommand, IResult> {
			private readonly IUserGroupWriteRepository userGroupWriteRepository;
			private readonly IMapper mapper;

			public CreateGroupOperationClaimsCommandHandler(IUserGroupWriteRepository userGroupWriteRepository,
				IMapper mapper) {
				this.userGroupWriteRepository = userGroupWriteRepository;
				this.mapper = mapper;
			}

			[SecuredAspect(Priority = 1)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(CreateUserGroupOperationClaimsCommand request, CancellationToken cancellationToken) {
				foreach(var claim in request.UserGroups) {
					await this.userGroupWriteRepository.AddAsync(this.mapper.Map<UserGroup>(claim)); //TODO: test edilecek
				}
				;
				await this.userGroupWriteRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Added);
			}
		}
	}
}