using AutoMapper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.ProductImages;
using Entities.DTOs.ProductImage;
using MediatR;

namespace Business.Handlers.ProductImages.Queries {
	public class GetProductImageDetailsQuery : IRequest<IDataResult<IEnumerable<GetProductImageDto>>> {
		public Guid ProductId { get; set; }
		public class GetProductImageDetailsQueryHandler : IRequestHandler<GetProductImageDetailsQuery, IDataResult<IEnumerable<GetProductImageDto>>> {
			private readonly IProductImageReadRepository productImageReadRepository;
			private readonly IMapper mapper;

			public GetProductImageDetailsQueryHandler(
				IProductImageReadRepository productImageReadRepository,
				IMapper mapper) {
				this.productImageReadRepository = productImageReadRepository;
				this.mapper = mapper;
			}

			//[SecuredAspect(Priority = 1)]
			public async Task<IDataResult<IEnumerable<GetProductImageDto>>> Handle(GetProductImageDetailsQuery request, CancellationToken cancellationToken) {
				var productImage = await this.productImageReadRepository.GetProductImagesDetailsByProductIdAsync(request.ProductId);

				var productImageDto = this.mapper.Map<IEnumerable<GetProductImageDto>>(productImage);

				return new SuccessDataResult<IEnumerable<GetProductImageDto>>(productImageDto);
			}
		}
	}
}
