using AutoMapper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.Products;
using Entities.DTOs.Product;
using MediatR;

namespace Business.Handlers.Products.Queries {
	/// <summary>
	/// GetProductDetailsByIdDto
	/// </summary>
	public class GetProductDetailsQuery : IRequest<IDataResult<GetProductDto>> {
		public Guid Id { get; set; }

		public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, IDataResult<GetProductDto>> {
			private readonly IProductReadRepository productReadRepository;
			private readonly IMapper mapper;

			public GetProductDetailsQueryHandler(IProductReadRepository productReadRepository, IMapper mapper) {
				this.productReadRepository = productReadRepository;
				this.mapper = mapper;
			}

			//[SecuredAspect(Priority = 1)]
			public async Task<IDataResult<GetProductDto>> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken) {
				var product = await this.productReadRepository.GetProductDetailsById(request.Id);

				var productDto = this.mapper.Map<GetProductDto>(product);

				return new SuccessDataResult<GetProductDto>(productDto);
			}
		}
	}
}