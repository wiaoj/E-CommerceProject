using AutoMapper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.ProductImages;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.ProductImages.Queries {
	public class GetProductImageQuery : IRequest<IDataResult<IEnumerable<ProductImage>>> {
		public Guid ProductId { get; set; }
		public class GetProductImageQueryHandler : IRequestHandler<GetProductImageQuery, IDataResult<IEnumerable<ProductImage>>> {
			private readonly IProductImageReadRepository productImageReadRepository;
			private readonly IMapper mapper;

			public GetProductImageQueryHandler(
				IProductImageReadRepository productImageReadRepository,
				IMapper mapper) {
				this.productImageReadRepository = productImageReadRepository;
				this.mapper = mapper;
			}

			//[SecuredAspect(Priority = 1)]
			public async Task<IDataResult<IEnumerable<ProductImage>>> Handle(GetProductImageQuery request, CancellationToken cancellationToken) {
				var productImage = await this.productImageReadRepository.GetProductImagesDetailsByProductIdAsync(request.ProductId);

				//var productImageDto = this.mapper.Map<IEnumerable<GetProductImageDto>>(productImage);

				return new SuccessDataResult<IEnumerable<ProductImage>>(productImage);
			}
		}
	}
}