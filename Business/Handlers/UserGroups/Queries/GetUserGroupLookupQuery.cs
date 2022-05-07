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
	public class GetUserGroupLookupQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>> {
		public Guid Id { get; set; }

		public class GetUserGroupLookupQueryHandler : IRequestHandler<GetUserGroupLookupQuery, IDataResult<IEnumerable<SelectionItem>>> {
			private readonly IUserGroupReadRepository userGroupReadRepository;

			public GetUserGroupLookupQueryHandler(IUserGroupReadRepository userGroupReadRepository) {
				this.userGroupReadRepository = userGroupReadRepository;
			}

			[SecuredAspect(Priority = 1)]
			[CacheAspect(10)]
			public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetUserGroupLookupQuery request, CancellationToken cancellationToken) {
				var data = await this.userGroupReadRepository.GetUserGroupSelectedList(request.Id);
				return new SuccessDataResult<IEnumerable<SelectionItem>>(data);
			}
		}
	}
}