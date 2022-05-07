using Business.Handlers.Products.Messages;
using Business.Handlers.Products.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.Products;
using MediatR;

namespace Business.Handlers.Products.Commands {
	public class DeleteProductCommand : IRequest<IResult> {
		public Guid Id { get; set; }

		public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, IResult> {
			private readonly IProductWriteRepository productWriteRepository;

			public DeleteProductCommandHandler(IProductWriteRepository productWriteRepository) {
				this.productWriteRepository = productWriteRepository;
			}

			//[SecuredAspect(Priority = 1)]
			[ValidationAspect(typeof(DeleteProductValidator), Priority = 2)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken) {
				await this.productWriteRepository.RemoveAsync(request.Id);
				return new SuccessResult(ProductMessages.Deleted);
			}
		}
	}
}