using Business.Constants;
using Business.Handlers.ProductImages.Messages;
using Business.Handlers.ProductImages.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract;
using DataAccess.Abstract.Repositories.ProductImages;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Business.Handlers.ProductImages.Commands {
	public class UpdateProductImageCommand : IRequest<IResult> {
		public Guid Id { get; set; }
		//public String ImagePath { get; set; }
		public IFormFile Image { get; set; }

		public class UpdateProductImageCommandHandler : IRequestHandler<UpdateProductImageCommand, IResult> {
			private readonly IProductImageReadRepository productImageReadRepository;
			private readonly IProductImageWriteRepository productImageWriteRepository;
			private readonly IFileHelper fileHelper;

			public UpdateProductImageCommandHandler(IProductImageReadRepository productImageReadRepository, IProductImageWriteRepository productImageWriteRepository, IFileHelper fileHelper) {
				this.productImageReadRepository = productImageReadRepository;
				this.productImageWriteRepository = productImageWriteRepository;
				this.fileHelper = fileHelper;
			}

			//[SecuredAspect(Priority = 1)]
			[ValidationAspect(typeof(UpdateProductImageValidator), Priority = 2)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(UpdateProductImageCommand request, CancellationToken cancellationToken) {
				var isThereAnyProductImage = await this.productImageReadRepository.GetByIdAsync(request.Id);
				//TODO: Code refactoring
				//isThereAnyProductImage!.ImagePath = request.ImagePath;
				var asd = Paths.ProductImagesPathWithProductId(Guid.Parse("912164e3-cba1-4075-a87f-27ba1f96a48f"));

				isThereAnyProductImage.ImagePath = await this.fileHelper.Update(@$"{asd}{isThereAnyProductImage.ImagePath}", $"{asd}", request.Image);


				this.productImageWriteRepository.Update(isThereAnyProductImage);
				await this.productImageWriteRepository.SaveChangesAsync();
				return new SuccessResult(ProductImageMessages.Updated);
			}
		}
	}
}