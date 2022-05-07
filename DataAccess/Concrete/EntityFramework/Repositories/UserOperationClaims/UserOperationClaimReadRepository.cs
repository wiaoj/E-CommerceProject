using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using DataAccess.Abstract.Repositories.UserOperationClaims;

namespace DataAccess.Concrete.EntityFramework.Repositories.UserOperationClaims {
	public class UserOperationClaimReadRepository : EfEntityReadRepositoryBase<UserOperationClaim, DataBaseContext>, IUserOperationClaimReadRepository {
		public UserOperationClaimReadRepository(DataBaseContext context) : base(context) { }

		public Task<IQueryable<SelectionItem>> GetUserOperationClaimSelectedList(Guid id) {
			//IEnumerable<SelectionItem> list =
			//    await(from operationClaim in Context.OperationClaims
			//          join userClaims in Context.UserOperationClaims on operationClaim.Id equals userClaims.OperationClaimId
			//          where userClaims.UserId.Equals(id)
			//          select new SelectionItem() {
			//              Id = operationClaim.Id,
			//              Label = operationClaim.Name
			//          }).ToListAsync();
			//return list;
			return null;
		}
	}
}