using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Securing;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Entities.Dtos;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract;
using DataAccess.Abstract.Repositories.Users;
using MediatR;

namespace Business.Handlers.Users.Queries {
	public class GetUserLookupQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>> {
		public class GetUserLookupQueryHandler : IRequestHandler<GetUserLookupQuery, IDataResult<IEnumerable<SelectionItem>>> {
			private readonly IUserReadRepository userReadRepository;

			public GetUserLookupQueryHandler(IUserReadRepository userReadRepository) {
				this.userReadRepository = userReadRepository;
			}

			[SecuredAspect(Priority = 1)]
			[CacheAspect(10)]
			public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetUserLookupQuery request, CancellationToken cancellationToken) {
				var list = this.userReadRepository.GetWhere(x => x.Status);
				var item = list.Select(x => new SelectionItem() {
					Id = x.Id.ToString(),
					Label = $"{x.FirstName} {x.LastName}",
					IsDisabled = x.Status.Equals(default)
				});
				return new SuccessDataResult<IEnumerable<SelectionItem>>(item);
			}
		}
	}
}