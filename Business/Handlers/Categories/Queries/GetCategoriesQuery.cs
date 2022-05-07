using AutoMapper;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.Categories;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.Categories.Queries {
	public class GetCategoriesQuery : IRequest<IDataResult<IQueryable<Category>>> {
		public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IDataResult<IQueryable<Category>>> {
			private readonly ICategoryReadRepository categoryReadRepository;
			private readonly IMapper mapper;

			public GetCategoriesQueryHandler(ICategoryReadRepository categoryReadRepository,
				IMapper mapper) {
				this.categoryReadRepository = categoryReadRepository;
				this.mapper = mapper;
			}

			//[SecuredAspect(Priority = 1)]
			[CacheAspect(20)]
			public async Task<IDataResult<IQueryable<Category>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken) {
				var categories = this.categoryReadRepository.GetAll();

				return new SuccessDataResult<IQueryable<Category>>(categories);
			}
		}
	}
}