using Business.Constants;
using Business.Handlers.ProductImages.Messages;
using Business.Handlers.ProductImages.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Error;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.ProductImages;
using DataAccess.Abstract.Repositories.Products;
using Entities.Concrete;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Business.Handlers.ProductImages.Commands {
	public class CreateProductImageCommand : IRequest<IResult> {
		public Guid ProductId { get; set; }
		public IFormFile Image { get; set; }
		public class CreateProductImageCommandHandler : IRequestHandler<CreateProductImageCommand, IResult> {
			private readonly IProductReadRepository productReadRepository;
			private readonly IProductImageWriteRepository productImageWriteRepository;
			private readonly IProductImageReadRepository productImageReadRepository;
			private readonly IFileHelper fileHelper;
			private readonly IBusinessRules businessRules;

			public CreateProductImageCommandHandler(
				IProductReadRepository productReadRepository,
				IProductImageWriteRepository productImageWriteRepository,
				IProductImageReadRepository productImageReadRepository,
				IBusinessRules businessRules,
				IFileHelper fileHelper) {
				this.productReadRepository = productReadRepository;
				this.productImageWriteRepository = productImageWriteRepository;
				this.productImageReadRepository = productImageReadRepository;
				this.fileHelper = fileHelper;
				this.businessRules = businessRules;
			}

			//[SecuredAspect(Priority = 1)]
			[ValidationAspect(typeof(CreateProductImageValidator), Priority = 2)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(CreateProductImageCommand request, CancellationToken cancellationToken) {
				//TODO: Code refactoring???
				var product = await this.productReadRepository.GetByIdAsync(request.ProductId);

				if(product is null)
					return new ErrorResult();

				IResult? result = this.businessRules.Run(await this.CheckProductImagesCount(product.Id));

				if(result is not null)
					return result;

				var asd = Paths.ProductImagesPathWithProductId(request.ProductId);
				//var imageId = Guid.NewGuid();
				ProductImage productsImage = new() {
					//Id = imageId,
					ImagePath = @$"{product.Id}\{await this.fileHelper.Upload($"{asd}",/* imageId,*/ request.Image)}",
					Product = product
				};

				//TODO product not update -> product cache problem
				await this.productImageWriteRepository.AddAsync(productsImage);
				await this.productImageWriteRepository.SaveChangesAsync();
				return new SuccessResult(ProductImageMessages.Added);
			}

			private async Task<IResult> CheckProductImagesCount(Guid productId) {
				var result = await this.productImageReadRepository.GetCountAsync(image => image.Product.Id.Equals(productId));
				return result < 10 ? new SuccessResult() : new ErrorResult(ProductImageMessages.MaximumImageCount(10));
			}
		}
	}
}