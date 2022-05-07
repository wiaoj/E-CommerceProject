using AutoMapper;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract;
using DataAccess.Abstract.Repositories.Products;
using Entities.DTOs.Product;
using MediatR;

namespace Business.Handlers.Products.Queries {
	public class GetProductsDetailsQuery : IRequest<IDataResult<IEnumerable<ListProductDto>>> {

		public class GetProductsDetailsQueryHandler : IRequestHandler<GetProductsDetailsQuery, IDataResult<IEnumerable<ListProductDto>>> {
			private readonly IProductReadRepository IProductReadRepository;
			private readonly IMapper mapper;

			public GetProductsDetailsQueryHandler(
				IProductReadRepository IProductReadRepository,
				IMapper mapper) {
				this.IProductReadRepository = IProductReadRepository;
				this.mapper = mapper;
			}

			//[SecuredAspect(Priority = 1)]
			[CacheAspect(10)]
			public async Task<IDataResult<IEnumerable<ListProductDto>>> Handle(GetProductsDetailsQuery request, CancellationToken cancellationToken) {
				var productList = this.IProductReadRepository.GetProductsDetails();

				var productDtoList = this.mapper.Map<IEnumerable<ListProductDto>>(productList);

				return new SuccessDataResult<IEnumerable<ListProductDto>>(productDtoList);
			}
		}
	}
}