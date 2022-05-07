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
	public class UpdateUserGroupByGroupIdCommand : IRequest<IResult> {
		public Guid Id { get; set; }
		public Guid GroupId { get; set; }
#pragma warning disable CS8618 // Non-nullable property 'UserIds' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public Guid[] UserIds { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'UserIds' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

		public class UpdateUserGroupByGroupIdCommandHandler : IRequestHandler<UpdateUserGroupByGroupIdCommand, IResult> {
			private readonly IUserGroupWriteRepository userGroupWriteRepository;
			private readonly IMapper mapper;

			public UpdateUserGroupByGroupIdCommandHandler(IUserGroupWriteRepository userGroupWriteRepository,
				IMapper mapper) {
				this.userGroupWriteRepository = userGroupWriteRepository;
				this.mapper = mapper;
			}

			[SecuredAspect(Priority = 1)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(UpdateUserGroupByGroupIdCommand request, CancellationToken cancellationToken) {
				var list = request.UserIds.Select(x =>
				new UserGroup() {
					UserId = x,
					GroupId = request.GroupId //TODO:
				});
				await this.userGroupWriteRepository.BulkInsertByGroupId(request.GroupId, list);
				await this.userGroupWriteRepository.SaveChangesAsync();

				return new SuccessResult(Messages.Updated);
			}
		}
	}
}