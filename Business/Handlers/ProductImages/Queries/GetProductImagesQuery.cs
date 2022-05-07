using AutoMapper;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.ProductImages;
using Entities.DTOs.ProductImage;
using MediatR;

namespace Business.Handlers.ProductImages.Queries {
	public class GetProductImagesQuery : IRequest<IDataResult<IEnumerable<ListProductImagesDto>>> {

		public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQuery, IDataResult<IEnumerable<ListProductImagesDto>>> {
			private readonly IProductImageReadRepository productImageReadRepository;
			private readonly IMapper mapper;

			public GetProductImagesQueryHandler(
				IProductImageReadRepository productImageReadRepository,
				IMapper mapper) {
				this.productImageReadRepository = productImageReadRepository;
				this.mapper = mapper;
			}

			//[SecuredAspect(Priority = 1)]
			[CacheAspect(10)]
			public async Task<IDataResult<IEnumerable<ListProductImagesDto>>> Handle(GetProductImagesQuery request, CancellationToken cancellationToken) {
				var productImageList = this.productImageReadRepository.GetAll();

				var productImagesDto = this.mapper.Map<IEnumerable<ListProductImagesDto>>(productImageList);

				return new SuccessDataResult<IEnumerable<ListProductImagesDto>>(productImagesDto);
			}
		}
	}
}