using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.Repositories.Products;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework.Repositories.Products {
	public class ProductWriteRepository : EfEntityWriteRepositoryBase<Product, DataBaseContext>, IProductWriteRepository {
		public ProductWriteRepository(DataBaseContext context) : base(context) { }
	}
}