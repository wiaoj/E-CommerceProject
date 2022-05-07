using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Securing;
using Core.Entities.Dtos;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.UserGroups;
using MediatR;

namespace Business.Handlers.UserGroups.Queries {
	public class GetUserGroupLookupByUserIdQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>> {
		public Guid UserId { get; set; }
		public class GetUserGroupLookupByUserIdQueryHandler : IRequestHandler<GetUserGroupLookupByUserIdQuery,
			IDataResult<IEnumerable<SelectionItem>>> {
			private readonly IUserGroupReadRepository userGroupReadRepository;
			private readonly IMediator mediator;

			public GetUserGroupLookupByUserIdQueryHandler(IUserGroupReadRepository userGroupReadRepository, IMediator mediator) {
				this.userGroupReadRepository = userGroupReadRepository;
				this.mediator = mediator;
			}

			[SecuredAspect(Priority = 1)]
			[CacheAspect(10)]
			public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetUserGroupLookupByUserIdQuery request, CancellationToken cancellationToken) {
				var data = await this.userGroupReadRepository.GetUserGroupSelectedList(request.UserId);
				return new SuccessDataResult<IEnumerable<SelectionItem>>(data);
			}
		}
	}
}