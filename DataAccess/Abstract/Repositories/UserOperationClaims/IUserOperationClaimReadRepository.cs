using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Entities.Dtos;

namespace DataAccess.Abstract.Repositories.UserOperationClaims {
	public interface IUserOperationClaimReadRepository : IEntityReadRepository<UserOperationClaim> {
		Task<IQueryable<SelectionItem>> GetUserOperationClaimSelectedList(Guid id);
	}
}