using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract.Repositories.UserOperationClaims;

namespace DataAccess.Concrete.EntityFramework.Repositories.UserOperationClaims {
	public class UserOperationClaimWriteRepository : EfEntityWriteRepositoryBase<UserOperationClaim, DataBaseContext>, IUserOperationClaimWriteRepository {
		public UserOperationClaimWriteRepository(DataBaseContext context) : base(context) { }

		public async Task BulkInsert(Guid userId, IEnumerable<UserOperationClaim> userClaims) {
			IQueryable<UserOperationClaim> dataBaseClaimList = this.Table.Where(x => x.UserId.Equals(userId)).AsQueryable();

			this.RemoveRange(dataBaseClaimList);
			await this.AddRangeAsync(userClaims);
		}
	}
}