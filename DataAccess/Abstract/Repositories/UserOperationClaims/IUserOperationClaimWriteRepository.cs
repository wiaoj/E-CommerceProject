using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract.Repositories.UserOperationClaims {
	public interface IUserOperationClaimWriteRepository : IEntityWriteRepository<UserOperationClaim> {
		Task BulkInsert(Guid id, IEnumerable<UserOperationClaim> userClaims);
	}
}