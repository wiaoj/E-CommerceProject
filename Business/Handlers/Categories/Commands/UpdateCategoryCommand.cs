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
	public class UpdateCategoryCommand : IRequest<IResult> {
		public Guid Id { get; set; }
		public String Name { get; set; }

		private class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, IResult> {
			private readonly ICategoryWriteRepository categoryWriteRepository;
			private readonly IMapper mapper;
			public UpdateCategoryCommandHandler(ICategoryWriteRepository categoryWriteRepository,
				IMapper mapper) {
				this.categoryWriteRepository = categoryWriteRepository;
				this.mapper = mapper;
			}

			//[SecuredAspect(Priority = 1)]
			[ValidationAspect(typeof(UpdateCategoryValidator), Priority = 2)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken) {
				//var category = await categoryRepository.GetByIdAsync(request.Id);
				//var category = await this.categoryReadRepository.GetByIdAsync(request.Id);

				var category = this.mapper.Map<Category>(request);
				//category.Name = request.Name;
				this.categoryWriteRepository.Update(category);
				await this.categoryWriteRepository.SaveChangesAsync();
				return new SuccessResult(CategoryMessages.Updated);
			}
		}
	}
}
