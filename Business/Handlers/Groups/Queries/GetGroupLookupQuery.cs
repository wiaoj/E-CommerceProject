using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Securing;
using Core.Entities.Dtos;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.Groups;
using MediatR;

namespace Business.Handlers.Groups.Queries {
	public class GetGroupLookupQuery : IRequest<IDataResult<IQueryable<SelectionItem>>> {
		public class GetGroupSelectListQueryHandler : IRequestHandler<GetGroupLookupQuery, IDataResult<IQueryable<SelectionItem>>> {
			private readonly IGroupReadRepository groupReadRepository;
			public GetGroupSelectListQueryHandler(IGroupReadRepository groupRepository) {
				this.groupReadRepository = groupRepository;
			}

			[SecuredAspect(Priority = 1)]
			[CacheAspect(10)]
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
			public async Task<IDataResult<IQueryable<SelectionItem>>> Handle(GetGroupLookupQuery request, CancellationToken cancellationToken) {
				var list = this.groupReadRepository.GetAll(false);
				var item = list.Select(x => new SelectionItem() {
					Id = x.Id.ToString(),
					Label = x.Name
				});
				return new SuccessDataResult<IQueryable<SelectionItem>>(item);
			}
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
		}
	}
}