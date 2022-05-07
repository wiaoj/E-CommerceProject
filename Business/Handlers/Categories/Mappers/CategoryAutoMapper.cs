using AutoMapper;
using Business.Handlers.Categories.Commands;
using Entities.Concrete;
using Entities.DTOs.Category;

namespace Business.Handlers.Categories.Mappers {
	internal class CategoryAutoMapper : Profile {
		public CategoryAutoMapper() {
			this.CreateMap<Category, CreateCategoryCommand>().ReverseMap();
			//CreateMap<Category, UpdateCategoryCommand>();
			//this.CreateMap<Category, UpdateCategoryCommand>();
			this.CreateMap<Category, UpdateCategoryCommand>().ReverseMap();
			// .ForMember(x => x.Name, x => x.Ignore())
			//this.CreateMap<Category, DeleteCategoryCommand>().ReverseMap();

			this.CreateMap<Category, GetCategoryDto>()
			//.ForMember(categoryDto => categoryDto.Products, option => option.MapFrom(category => category.Products))
			.ReverseMap();

			this.CreateMap<Category, ListCategoryDto>()
				.ForMember(categoryDto => categoryDto.ProductCount,
				option => option.MapFrom(category => category.Products.Count))
				.ReverseMap();
		}
	}
}