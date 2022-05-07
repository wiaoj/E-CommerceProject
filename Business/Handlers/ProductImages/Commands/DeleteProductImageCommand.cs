using Business.Handlers.ProductImages.Messages;
using Business.Handlers.ProductImages.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract;
using DataAccess.Abstract.Repositories.ProductImages;
using MediatR;

namespace Business.Handlers.ProductImages.Commands {
	public class DeleteProductImageCommand : IRequest<IResult> {
		public Guid Id { get; set; }

		public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommand, IResult> {
			private readonly IProductImageWriteRepository productImageWriteRepository;

			public DeleteProductImageCommandHandler(IProductImageWriteRepository productImageWriteRepository) {
				this.productImageWriteRepository = productImageWriteRepository;
			}

			//[SecuredAspect(Priority = 1)]
			[ValidationAspect(typeof(DeleteProductImageValidator), Priority = 2)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken) {
				await this.productImageWriteRepository.RemoveAsync(request.Id);
				return new SuccessResult(ProductImageMessages.Deleted);
			}
		}
	}
}