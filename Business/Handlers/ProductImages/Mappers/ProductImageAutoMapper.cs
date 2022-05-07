using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.ProductImage;

namespace Business.Handlers.ProductImages.Mappers {
	internal class ProductImageAutoMapper : Profile {
		public ProductImageAutoMapper() {
			this.CreateMap<ProductImage, GetProductImageDto>()
				.ForMember(productImageDto => productImageDto.ProductId, option => option.MapFrom(productImage => productImage.Product.Id))
				.ReverseMap();

			this.CreateMap<ProductImage, ListProductImagesDto>()
				.ForMember(productImagesDto => productImagesDto.ProductId, option => option.MapFrom(productImage => productImage.Product.Id))
				.ReverseMap();
		}
	}
}