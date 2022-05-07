using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract.Repositories.Categories {
	public interface ICategoryReadRepository : IEntityReadRepository<Category> {
		public IQueryable<Category> GetByCategoryIdWithProducts(Guid id);
	}
}