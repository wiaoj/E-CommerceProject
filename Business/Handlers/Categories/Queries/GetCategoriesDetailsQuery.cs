using AutoMapper;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Pagination;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Error;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.Categories;
using Entities.DTOs.Category;
using MediatR;

namespace Business.Handlers.Categories.Queries {
	public class GetCategoriesDetailsQuery : IPagination, IRequest<IDataResult<IQueryable<ListCategoryDto>>> {
		public Int32 Page { get; set; }
		public Byte Size { get; set; }

		public class GetCategoriesDetailsQueryHandler : IRequestHandler<GetCategoriesDetailsQuery, IDataResult<IQueryable<ListCategoryDto>>> {
			private readonly ICategoryReadRepository categoryReadRepository;
			private readonly IMapper mapper;
			public GetCategoriesDetailsQueryHandler(ICategoryReadRepository categoryReadRepository,
				IMapper mapper) {
				this.categoryReadRepository = categoryReadRepository;
				this.mapper = mapper;
			}

			//[SecuredAspect(Priority = 1)]
			//[CacheAspect(20)]
			public async Task<IDataResult<IQueryable<ListCategoryDto>>> Handle(GetCategoriesDetailsQuery request, CancellationToken cancellationToken) {
				//var categories = await this.categoryRepository.GetListAsync();
				//var categoriesDto = this.mapper.Map<IEnumerable<ListCategoryDto>>(categories);
				var categories = this.categoryReadRepository.GetAll(tracking: true);
				var categoriesPagination = categories.Skip(request.Page * request.Size).Take(request.Size);
				var categoriesDto = this.mapper.ProjectTo<ListCategoryDto>(categoriesPagination);
				return new PaginationResult<IQueryable<ListCategoryDto>>(categoriesDto, request.Page, request.Size, categories.Count());
				//return new SuccessDataResult<IEnumerable<ListCategoryDto>>(categoriesDto/*, ResultStatus.Information*/);
			}
		}
	}
}