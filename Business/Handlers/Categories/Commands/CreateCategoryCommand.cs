using AutoMapper;
using Business.Handlers.Categories.Messages;
using Business.Handlers.Categories.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.Categories;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.Categories.Commands {
	public class CreateCategoryCommand : IRequest<IResult> {
		public String Name { get; set; }

		private class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, IResult> {
			private readonly ICategoryWriteRepository categoryWriteRepository;
			private readonly IMapper mapper;
			public CreateCategoryCommandHandler(ICategoryWriteRepository categoryWriteRepository,
				IMapper mapper) {
				this.categoryWriteRepository = categoryWriteRepository;
				this.mapper = mapper;
			}

			//[SecuredAspect(Priority = 1)]
			[ValidationAspect(typeof(CreateCategoryValidator), Priority = 2)]
			[CacheRemoveAspect("Business.Handlers.Categories.Queries.GetCategoriesQuery.Handle(System.Threading.CancellationToken_False_False_System.Threading.ManualResetEvent)")]
			public async Task<IResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken) {
				var category = this.mapper.Map<Category>(request);

				var asd = await this.categoryWriteRepository.AddAsync(category);
				var assd = await this.categoryWriteRepository.SaveChangesAsync();
				Queue<IResult> result = new(); //TODO
				result.Enqueue(asd);
				result.Enqueue(assd);
				return new SuccessResult(CategoryMessages.Added);
			}
		}
	}
}