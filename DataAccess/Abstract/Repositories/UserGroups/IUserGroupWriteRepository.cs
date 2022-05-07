using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract.Repositories.UserGroups {
	public interface IUserGroupWriteRepository : IEntityWriteRepository<UserGroup> {
		Task BulkInsert(Guid userId, IEnumerable<UserGroup> userGroups);
		Task BulkInsertByGroupId(Guid groupId, IEnumerable<UserGroup> userGroups);
	}
}