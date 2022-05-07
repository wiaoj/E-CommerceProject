using Business.Handlers.Categories.Messages;
using Business.Handlers.Categories.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.Categories;
using MediatR;

namespace Business.Handlers.Categories.Commands {
	public class DeleteCategoryCommand : IRequest<IResult> {
		public Guid Id { get; set; }

		private class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, IResult> {
			private readonly ICategoryWriteRepository categoryWriteRepository;
			public DeleteCategoryCommandHandler(ICategoryWriteRepository categoryWriteRepository) {
				this.categoryWriteRepository = categoryWriteRepository;
			}

			//[SecuredAspect(Priority = 1)]
			[ValidationAspect(typeof(DeleteCategoryValidator), Priority = 2)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken) {
				//var category = this.mapper.Map<Category>(request);

				await this.categoryWriteRepository.RemoveAsync(request.Id);
				await this.categoryWriteRepository.SaveChangesAsync();
				return new SuccessResult(CategoryMessages.Deleted);
			}
		}
	}
}