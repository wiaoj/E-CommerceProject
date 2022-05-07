using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract.Repositories.GroupClaims {
	public interface IGroupClaimWriteRepository : IEntityWriteRepository<GroupOperationClaim> {
		Task BulkInsert(Guid groupId, IEnumerable<GroupOperationClaim> groupClaims);
	}
}