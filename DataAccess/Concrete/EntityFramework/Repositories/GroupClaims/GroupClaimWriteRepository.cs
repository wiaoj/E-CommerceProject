using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract.Repositories.GroupClaims;

namespace DataAccess.Concrete.EntityFramework.Repositories.GroupClaims {
	public class GroupClaimWriteRepository : EfEntityWriteRepositoryBase<GroupOperationClaim, DataBaseContext>, IGroupClaimWriteRepository {
		public GroupClaimWriteRepository(DataBaseContext context) : base(context) { }

		public async Task BulkInsert(Guid groupId, IEnumerable<GroupOperationClaim> groupClaims) {
			IQueryable<GroupOperationClaim> dataBaseList = this.Table.Where(x => x.GroupId.Equals(groupId)).AsQueryable();

			this.RemoveRange(dataBaseList);
			await this.AddRangeAsync(groupClaims);
		}
	}
}