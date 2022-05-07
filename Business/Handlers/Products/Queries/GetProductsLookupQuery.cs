using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Securing;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Entities.Dtos;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract;
using DataAccess.Abstract.Repositories.Products;
using MediatR;

namespace Business.Handlers.Products.Queries {
	public class GetProductsLookupQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>> {

		public class GetProductsLookupQueryHandler : IRequestHandler<GetProductsLookupQuery, IDataResult<IEnumerable<SelectionItem>>> {
			private readonly IProductReadRepository IProductReadRepository;

			public GetProductsLookupQueryHandler(IProductReadRepository IProductReadRepository) {
				this.IProductReadRepository = IProductReadRepository;
			}

			//[SecuredAspect(Priority = 1)]
			public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetProductsLookupQuery request, CancellationToken cancellationToken) {
				var list = this.IProductReadRepository.GetAll();

				var item = list.Select(x => new SelectionItem {
					Id = x.Id,
					Label = x.Name
				});
				return new SuccessDataResult<IEnumerable<SelectionItem>>(item);
			}
		}
	}
}