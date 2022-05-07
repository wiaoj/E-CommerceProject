using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract.Repositories.Products {
	public interface IProductReadRepository : IEntityReadRepository<Product> {
		public Task<Product?> GetProductDetailsById(Guid id);
		public IQueryable<Product> GetProductsDetails();
	}
}