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
	public class CreateProductCommand : IRequest<IResult> {
		public String Name { get; set; }
		public Guid CategoryId { get; set; }
		public Decimal UnitPrice { get; set; }
		public Int16 UnitInStock { get; set; }
		public String Description { get; set; }

		public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, IResult> {
			private readonly IProductWriteRepository productWriteRepository;
			private readonly IMapper mapper;

			public CreateProductCommandHandler(IProductWriteRepository productWriteRepository,
				IMapper mapper) {
				this.productWriteRepository = productWriteRepository;
				this.mapper = mapper;
			}

			//[SecuredAspect(Priority = 1)]
			[ValidationAspect(typeof(CreateProductValidator), Priority = 2)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(CreateProductCommand request, CancellationToken cancellationToken) {

				var product = this.mapper.Map<Product>(request);
				//Product product = new() {
				//	Name = request.Name,
				//	Category = /*request.Category,*/
				//	new Category { Id = request.CategoryId, Name = "asdasdas" },
				//	UnitPrice = request.UnitPrice,
				//	UnitInStock = request.UnitInStock,
				//	Description = request.Description
				//};

				await this.productWriteRepository.AddAsync(product);
				await this.productWriteRepository.SaveChangesAsync();
				return new SuccessResult(ProductMessages.Added);
			}
		}
	}
}