using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract.Repositories.UserGroups;

namespace DataAccess.Concrete.EntityFramework.Repositories.UserGroups {
	public class UserGroupWriteRepository : EfEntityWriteRepositoryBase<UserGroup, DataBaseContext>, IUserGroupWriteRepository {
		public UserGroupWriteRepository(DataBaseContext context) : base(context) { }

		public async Task BulkInsert(Guid userId, IEnumerable<UserGroup> userGroups) {
			IQueryable<UserGroup> dataBaseUserGroupList = this.Table.Where(x => x.UserId.Equals(userId)).AsQueryable();

			this.RemoveRange(dataBaseUserGroupList);
			await this.AddRangeAsync(userGroups);
		}

		public async Task BulkInsertByGroupId(Guid groupId, IEnumerable<UserGroup> userGroups) {
			IQueryable<UserGroup> dataBaseUserGroupList = this.Table.Where(x => x.GroupId.Equals(groupId)).AsQueryable();

			this.RemoveRange(dataBaseUserGroupList);
			await this.AddRangeAsync(userGroups);
		}
	}
}