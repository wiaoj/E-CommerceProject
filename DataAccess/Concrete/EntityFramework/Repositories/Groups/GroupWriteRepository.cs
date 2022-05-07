using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract.Repositories.Groups;

namespace DataAccess.Concrete.EntityFramework.Repositories.Groups {
	public class GroupWriteRepository : EfEntityWriteRepositoryBase<Group, DataBaseContext>, IGroupWriteRepository {
		public GroupWriteRepository(DataBaseContext context) : base(context) { }
	}
}