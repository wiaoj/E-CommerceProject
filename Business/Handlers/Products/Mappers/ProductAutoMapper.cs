using AutoMapper;
using Business.Handlers.Products.Commands;
using Core.Entities.Dtos;
using Entities.Concrete;
using Entities.DTOs.Product;

namespace Business.Handlers.Products.Mappers {
	internal class ProductAutoMapper : Profile {
		public ProductAutoMapper() {
			this.CreateMap<Product, CreateProductCommand>()
				.ForMember(command => command.CategoryId, option =>
				  option.MapFrom(product => product.Category.Id))
				.ReverseMap();

			this.CreateMap<Product, UpdateProductCommand>()
				//.ForMember(command => command.CategoryId, option => option.MapFrom(product => product.Category.Id))
				.ReverseMap();
			//this.CreateMap<Product, DeleteProductCommand>().ReverseMap();


			this.CreateMap<Product, GetProductDto>();
				//.ForMember(productDto => productDto.CategoryName, option => option.MapFrom(product => product.Category.Name))
				//.ForMember(productDto => productDto.Images, option => option.MapFrom(product => product.Images.ToList()));
				//.ReverseMap();

			this.CreateMap<Product, ListProductDto>()
				.ForMember(productDto => productDto.CategoryName, option => option.MapFrom(product => product.Category.Name))
				.ForMember(productDto => productDto.Image, option => option.MapFrom(product => product.Images.FirstOrDefault()));

			this.CreateMap<Product, SelectionItem>()
				.ForMember(selectionItem => selectionItem.Id,
				 option => option.MapFrom(product => product.Id))
				.ForMember(selectionItem => selectionItem.Label,
				option => option.MapFrom(product => product.Name));
		}
	}
}
