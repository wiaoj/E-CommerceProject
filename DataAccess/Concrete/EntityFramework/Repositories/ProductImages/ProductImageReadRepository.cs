using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.Repositories.ProductImages;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Repositories.ProductImages {
	public class ProductImageReadRepository : EfEntityReadRepositoryBase<ProductImage, DataBaseContext>, IProductImageReadRepository {
		public ProductImageReadRepository(DataBaseContext context) : base(context) { }

		public Task<IQueryable<ProductImage>> GetProductImagesDetailsByProductIdAsync(Guid productId) {
			return Task.FromResult(this.Table
				.Include(productImage => productImage.Product)
				.Where(productImage => productImage.Product.Id.Equals(productId))
				.AsQueryable()
				.AsNoTracking());
		}

		public Task<IQueryable<ProductImage>> GetProductsImagesDetailsAsync() {
			return Task.FromResult(this.Table
					.Include(productImages => productImages.Product)
					.AsQueryable()
					.AsNoTracking());
		}
	}
}