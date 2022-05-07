using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using DataAccess.Abstract.Repositories.UserGroups;

namespace DataAccess.Concrete.EntityFramework.Repositories.UserGroups {
	public class UserGroupReadRepository : EfEntityReadRepositoryBase<UserGroup, DataBaseContext>, IUserGroupReadRepository {
		public UserGroupReadRepository(DataBaseContext context) : base(context) { }

		public Task<IEnumerable<SelectionItem>> GetUserGroupSelectedList(Guid userId) {
			//List<SelectionItem> list = await(from @group in Table
			//                                 join userGroup in Table on @group.Id equals userGroup.GroupId
			//                                 where userGroup.UserId.Equals(userId)
			//                                 select new SelectionItem() {
			//                                     Id = @group.Id.ToString(),
			//                                     Label = @group.Name
			//                                 }).ToListAsync();

			//return list;
#pragma warning disable CS8603 // Possible null reference return.
			return null;
#pragma warning restore CS8603 // Possible null reference return.
		}

		public Task<IEnumerable<SelectionItem>> GetUsersInGroupSelectedListByGroupId(Guid groupId) {
			//List<SelectionItem> list = await(from user in Context.Users
			//                                 join groupUser in Context.UserGroups on user.Id equals groupUser.UserId
			//                                 where groupUser.GroupId == groupId
			//                                 select new SelectionItem() {
			//                                     Id = user.Id.ToString(),
			//                                     Label = $"{user.FirstName} {user.LastName}"
			//                                 }).ToListAsync();
			//return list;
#pragma warning disable CS8603 // Possible null reference return.
			return null;
#pragma warning restore CS8603 // Possible null reference return.
		}
	}
}