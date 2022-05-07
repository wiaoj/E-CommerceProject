using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract.Repositories.Products {
	public interface IProductWriteRepository : IEntityWriteRepository<Product> { }
}