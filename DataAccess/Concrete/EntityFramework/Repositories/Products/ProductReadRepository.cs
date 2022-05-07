using Core.DataAccess.EntityFramework;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Abstract.Repositories.Products {
	public class ProductReadRepository : EfEntityReadRepositoryBase<Product, DataBaseContext>, IProductReadRepository {
		public ProductReadRepository(DataBaseContext context) : base(context) { }

		public async Task<Product?> GetProductDetailsById(Guid id) {
			return await this.Table
				.Include(product => product.Category)
				.Include(product => product.Images)
				.FirstOrDefaultAsync(x => x.Id.Equals(id));
		}

		public IQueryable<Product> GetProductsDetails() {
			return this.Table
				.Include(product => product.Category)
				.Include(product => product.Images)
				.AsQueryable()
				.AsNoTracking();
		}
	}
}