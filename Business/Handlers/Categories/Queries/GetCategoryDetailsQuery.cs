using AutoMapper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.Categories;
using Entities.DTOs.Category;
using MediatR;

namespace Business.Handlers.Categories.Queries {
	public class GetCategoryDetailsQuery : IRequest<IDataResult<IQueryable<GetCategoryDto>>> {
		public Guid Id { get; set; }

		public class GetCategoryDetailsQueryHandler : IRequestHandler<GetCategoryDetailsQuery, IDataResult<IQueryable<GetCategoryDto>>> {
			private readonly ICategoryReadRepository categoryReadRepository;
			private readonly IMapper mapper;

			public GetCategoryDetailsQueryHandler(ICategoryReadRepository categoryReadRepository,
				IMapper mapper) {
				this.categoryReadRepository = categoryReadRepository;
				this.mapper = mapper;
			}

			//[SecuredAspect(Priority = 1)]
			public async Task<IDataResult<IQueryable<GetCategoryDto>>> Handle(GetCategoryDetailsQuery request, CancellationToken cancellationToken) {
				//var categoryList = await this.categoryRepository.GetByIdWithProductsAsync(request.Id);
				var categoryList = this.categoryReadRepository.GetByCategoryIdWithProducts(request.Id);

				var categoryDto = this.mapper.ProjectTo<GetCategoryDto>(categoryList);

				return new SuccessDataResult<IQueryable<GetCategoryDto>>(categoryDto);
			}
		}
	}
}
