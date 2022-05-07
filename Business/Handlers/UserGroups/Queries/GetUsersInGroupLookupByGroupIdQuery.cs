using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Securing;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Entities.Dtos;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract;
using DataAccess.Abstract.Repositories.UserGroups;
using MediatR;

namespace Business.Handlers.UserGroups.Queries {
	public class GetUsersInGroupLookupByGroupIdQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>> {
		public Guid GroupId { get; set; }
		public class GetUsersInGroupLookupByGroupIdQueryHandler : IRequestHandler<GetUsersInGroupLookupByGroupIdQuery,
			IDataResult<IEnumerable<SelectionItem>>> {
			private readonly IUserGroupReadRepository userGroupReadRepository;

			public GetUsersInGroupLookupByGroupIdQueryHandler(IUserGroupReadRepository userGroupReadRepository) {
				this.userGroupReadRepository = userGroupReadRepository;
			}

			[SecuredAspect(Priority = 1)]
			[CacheAspect(10)]
			public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetUsersInGroupLookupByGroupIdQuery request, CancellationToken cancellationToken) {
				return new SuccessDataResult<IEnumerable<SelectionItem>>(await this.userGroupReadRepository.GetUsersInGroupSelectedListByGroupId(request.GroupId));
			}
		}
	}
}