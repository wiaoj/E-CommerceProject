using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.Repositories.Categories;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Repositories.Categories {
	public class CategoryReadRepository : EfEntityReadRepositoryBase<Category, DataBaseContext>, ICategoryReadRepository {
		public CategoryReadRepository(DataBaseContext context) : base(context) { }

		public IQueryable<Category> GetByCategoryIdWithProducts(Guid id) {
			return this.Table
				.Include(category => category.Products)
				.Where(category => category.Id.Equals(id)).AsQueryable();
		}
	}
}