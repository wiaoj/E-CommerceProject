using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract.Repositories.Users;

namespace DataAccess.Concrete.EntityFramework.Repositories.Users {
	public class UserReadRepository : EfEntityReadRepositoryBase<User, DataBaseContext>, IUserReadRepository {
		public UserReadRepository(DataBaseContext context) : base(context) { }

		public async Task<User?> GetByRefreshToken(String refreshToken) {
			return await this.GetSingleAsync(user => user.Status && user.RefreshToken.Equals(refreshToken));
		}

		public IQueryable<OperationClaim> GetClaims(Guid id) {
			//var result = (from user in Context.Users
			//              join userGroup in Context.UserGroups on user.Id equals userGroup.UserId
			//              join groupClaim in Context.GroupClaims on userGroup.GroupId equals groupClaim.GroupId
			//              join operationClaim in Context.OperationClaims on groupClaim.OperationClaimId equals operationClaim.Id
			//              where user.Id.Equals(userId)
			//              select new {
			//                  operationClaim.Name
			//              }).Union(from user in Context.Users
			//                       join userOperationClaim in Context.UserOperationClaims on user.Id equals userOperationClaim.UserId
			//                       join operationClaim in Context.OperationClaims on userOperationClaim.OperationClaimId equals operationClaim.Id
			//                       where user.Id.Equals(userId)
			//                       select new {
			//                           operationClaim.Name
			//                       });
			//return result.Select(x => new OperationClaim { Name = x.Name }).Distinct()
			//    .ToList
#pragma warning disable CS8603 // Possible null reference return.
			return null;
#pragma warning restore CS8603 // Possible null reference return.
		}
	}
}