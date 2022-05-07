using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Securing;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.Groups;
using MediatR;

namespace Business.Handlers.Groups.Queries {
	public class GetGroupsQuery : IRequest<IDataResult<IQueryable<Group>>> {
		public Guid Id { get; set; }

		public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery, IDataResult<IQueryable<Group>>> {
			private readonly IGroupReadRepository groupReadRepository;

			public GetGroupsQueryHandler(IGroupReadRepository groupReadRepository) {
				this.groupReadRepository = groupReadRepository;
			}

			[SecuredAspect(Priority = 1)]
			[CacheAspect(10)]
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
			public async Task<IDataResult<IQueryable<Group>>> Handle(GetGroupsQuery request, CancellationToken cancellationToken) {
				var list = this.groupReadRepository.GetAll(false);
				return new SuccessDataResult<IQueryable<Group>>(list);
			}
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
		}
	}
}