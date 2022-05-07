using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract.Repositories.ProductImages {
	public interface IProductImageReadRepository : IEntityReadRepository<ProductImage> {
		public Task<IQueryable<ProductImage>> GetProductImagesDetailsByProductIdAsync(Guid productId);
		public Task<IQueryable<ProductImage>> GetProductsImagesDetailsAsync();
	}
}