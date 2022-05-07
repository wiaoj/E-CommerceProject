using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using DataAccess.Abstract.Repositories.GroupClaims;

namespace DataAccess.Concrete.EntityFramework.Repositories.GroupClaims {
	public class GroupClaimReadRepository : EfEntityReadRepositoryBase<GroupOperationClaim, DataBaseContext>, IGroupClaimReadRepository {
		public GroupClaimReadRepository(DataBaseContext context) : base(context) { }

		public Task<IQueryable<SelectionItem>> GetGroupClaimsSelectedList(Guid id) {
			//var list = await(from groupClaim in Context.GroupClaims
			//				 join operationClaim in Context.OperationClaims on groupClaim.OperationClaimId equals operationClaim.Id
			//				 where groupClaim.GroupId.Equals(groupId)
			//				 select new SelectionItem() {
			//					 Id = operationClaim.Id.ToString(),
			//					 Label = operationClaim.Name
			//				 }).ToListAsync();
			//return list;
			return null;
		}
	}
}