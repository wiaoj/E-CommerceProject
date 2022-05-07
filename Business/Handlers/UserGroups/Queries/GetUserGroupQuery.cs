using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Securing;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.UserGroups;
using MediatR;

namespace Business.Handlers.UserGroups.Queries {
	public class GetUserGroupQuery : IRequest<IDataResult<UserGroup>> {
		public Guid Id { get; set; }

		public class GetUserGroupQueryHandler : IRequestHandler<GetUserGroupQuery, IDataResult<UserGroup>> {
			private readonly IUserGroupReadRepository userGroupReadRepository;

			public GetUserGroupQueryHandler(IUserGroupReadRepository userGroupReadRepository) {
				this.userGroupReadRepository = userGroupReadRepository;
			}

			[SecuredAspect(Priority = 1)]
			[CacheAspect(10)]
			public async Task<IDataResult<UserGroup>> Handle(GetUserGroupQuery request, CancellationToken cancellationToken) {
				var userGroup = await this.userGroupReadRepository.GetSingleAsync(p => p.UserId.Equals(request.Id));
				return new SuccessDataResult<UserGroup>(userGroup);
			}
		}
	}
}