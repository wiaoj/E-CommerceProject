using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.Repositories.Categories;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework.Repositories.Categories {
	public class CategoryWriteRepository : EfEntityWriteRepositoryBase<Category, DataBaseContext>, ICategoryWriteRepository {
		public CategoryWriteRepository(DataBaseContext context) : base(context) { }
	}
}