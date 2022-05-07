using AutoMapper;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.ProductImages;
using Entities.DTOs.ProductImage;
using MediatR;

namespace Business.Handlers.ProductImages.Queries {
	public class GetProductsImagesDetailsQuery : IRequest<IDataResult<IQueryable<ListProductImagesDto>>> {

		public class GetProductsImagesDetailsQueryHandler : IRequestHandler<GetProductsImagesDetailsQuery, IDataResult<IQueryable<ListProductImagesDto>>> {
			private readonly IProductImageReadRepository productImageReadRepository;
			private readonly IMapper mapper;

			public GetProductsImagesDetailsQueryHandler(
				IProductImageReadRepository productImageReadRepository,
				IMapper mapper) {
				this.productImageReadRepository = productImageReadRepository;
				this.mapper = mapper;
			}

			//[SecuredAspect(Priority = 1)]
			[CacheAspect(10)]
			public async Task<IDataResult<IQueryable<ListProductImagesDto>>> Handle(GetProductsImagesDetailsQuery request, CancellationToken cancellationToken) {
				var productImageList = await this.productImageReadRepository.GetProductsImagesDetailsAsync();

				var productImageListDtoList = this.mapper.ProjectTo<ListProductImagesDto>(productImageList);

				return new SuccessDataResult<IQueryable<ListProductImagesDto>>(productImageListDtoList);
			}
		}
	}
}