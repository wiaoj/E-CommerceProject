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
	public class CreateUserGroupCommand : IRequest<IResult> {
		public Guid GroupId { get; set; }
		public Guid UserId { get; set; }

		public class CreateUserGroupCommandHandler : IRequestHandler<CreateUserGroupCommand, IResult> {
			private readonly IUserGroupWriteRepository userGroupWriteRepository;
			private readonly IMapper mapper;

			public CreateUserGroupCommandHandler(IUserGroupWriteRepository userGroupWriteRepository,
				IMapper mapper) {
				this.userGroupWriteRepository = userGroupWriteRepository;
				this.mapper = mapper;
			}


			[SecuredAspect(Priority = 1)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(CreateUserGroupCommand request, CancellationToken cancellationToken) {
				//var userGroup = new UserGroup {
				//    GroupId = request.GroupId,
				//    UserId = request.UserId
				//};
				var userGroup = this.mapper.Map<UserGroup>(request);
				await this.userGroupWriteRepository.AddAsync(userGroup);
				await this.userGroupWriteRepository.SaveChangesAsync();

				return new SuccessResult(Messages.Added);
			}
		}
	}
}