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
	public class UpdateUserGroupCommand : IRequest<IResult> {
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
#pragma warning disable CS8618 // Non-nullable property 'GroupId' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public Guid[] GroupId { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'GroupId' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

		public class UpdateUserGroupCommandHandler : IRequestHandler<UpdateUserGroupCommand, IResult> {
			private readonly IUserGroupWriteRepository userGroupWriteRepository;
			private readonly IMapper mapper;

			public UpdateUserGroupCommandHandler(IUserGroupWriteRepository userGroupWriteRepository,
				IMapper mapper) {
				this.userGroupWriteRepository = userGroupWriteRepository;
				this.mapper = mapper;
			}

			[SecuredAspect(Priority = 1)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(UpdateUserGroupCommand request, CancellationToken cancellationToken) {
				var userGroupList = request.GroupId.Select(x =>
				new UserGroup() {
					GroupId = x,
					UserId = request.UserId
				});

				await this.userGroupWriteRepository.BulkInsert(request.UserId, userGroupList);
				await this.userGroupWriteRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Updated);
			}
		}
	}
}