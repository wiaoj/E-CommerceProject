using AutoMapper;
using Business.Handlers.Products.Messages;
using Business.Handlers.Products.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.Products;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.Products.Commands {
	public class UpdateProductCommand : IRequest<IResult> {
		public Guid Id { get; set; }
		public String Name { get; set; }
		public Guid CategoryId { get; set; }
		public Decimal UnitPrice { get; set; }
		public Int16 UnitInStock { get; set; }
		public String Description { get; set; }

		public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, IResult> {
			private readonly IProductWriteRepository productWriteRepository;
			private readonly IMapper mapper;

			public UpdateProductCommandHandler(IProductWriteRepository productWriteRepository,
				IMapper mapper) {
				this.productWriteRepository = productWriteRepository;
				this.mapper = mapper;
			}

			//[SecuredAspect(Priority = 1)]
			[ValidationAspect(typeof(UpdateProductValidator), Priority = 2)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken) {
				//var isThereAnyProduct = await this.productRepository.GetByIdAsync(request.Id);

				var product = this.mapper.Map<Product>(request);

				this.productWriteRepository.Update(product);
				await this.productWriteRepository.SaveChangesAsync();
				return new SuccessResult(ProductMessages.Updated);
			}
		}
	}
}