using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Entities.Dtos;

namespace DataAccess.Abstract.Repositories.UserGroups {
	public interface IUserGroupReadRepository : IEntityReadRepository<UserGroup> {
		Task<IEnumerable<SelectionItem>> GetUserGroupSelectedList(Guid userId);
		Task<IEnumerable<SelectionItem>> GetUsersInGroupSelectedListByGroupId(Guid groupId);
	}
}