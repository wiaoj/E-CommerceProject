using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract.Repositories.Groups;

namespace DataAccess.Concrete.EntityFramework.Repositories.Groups {
	public class GroupReadRepository : EfEntityReadRepositoryBase<Group, DataBaseContext>, IGroupReadRepository {
		public GroupReadRepository(DataBaseContext context) : base(context) { }
	}
}