using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Securing;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.UserGroups;
using MediatR;

namespace Business.Handlers.UserGroups.Queries {
	public class GetUserGroupsQuery : IRequest<IDataResult<IEnumerable<UserGroup>>> {
		public class GetUserGroupsQueryHandler : IRequestHandler<GetUserGroupsQuery, IDataResult<IEnumerable<UserGroup>>> {
			private readonly IUserGroupReadRepository userGroupReadRepository;

			public GetUserGroupsQueryHandler(IUserGroupReadRepository userGroupReadRepository) {
				this.userGroupReadRepository = userGroupReadRepository;
			}

			[SecuredAspect(Priority = 1)]
			[CacheAspect(10)]
			public async Task<IDataResult<IEnumerable<UserGroup>>> Handle(GetUserGroupsQuery request, CancellationToken cancellationToken) {
				return new SuccessDataResult<IEnumerable<UserGroup>>(this.userGroupReadRepository.GetAll());
			}
		}
	}
}