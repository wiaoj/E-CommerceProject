using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.Repositories.ProductImages;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework.Repositories.ProductImages {
	public class ProductImageWriteRepository : EfEntityWriteRepositoryBase<ProductImage, DataBaseContext>, IProductImageWriteRepository {
		public ProductImageWriteRepository(DataBaseContext context) : base(context) { }
	}
}