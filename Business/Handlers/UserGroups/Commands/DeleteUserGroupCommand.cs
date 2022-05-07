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
	public class DeleteUserGroupCommand : IRequest<IResult> {
		public Guid Id { get; set; }

		public class DeleteUserGroupCommandHandler : IRequestHandler<DeleteUserGroupCommand, IResult> {
			private readonly IUserGroupWriteRepository userGroupWriteRepository;
			private readonly IMapper mapper;

			public DeleteUserGroupCommandHandler(IUserGroupWriteRepository userGroupWriteRepository,
				IMapper mapper) {
				this.userGroupWriteRepository = userGroupWriteRepository;
				this.mapper = mapper;
			}

			[SecuredAspect(Priority = 1)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(DeleteUserGroupCommand request, CancellationToken cancellationToken) {
				//var entityToDelete = await this.userGroupWriteRepository.GetAsync(x => x.UserId.Equals(request.Id));
				var userGroup = this.mapper.Map<UserGroup>(request);
				this.userGroupWriteRepository.Remove(userGroup);
				await this.userGroupWriteRepository.SaveChangesAsync();

				return new SuccessResult(Messages.Deleted);
			}
		}
	}
}