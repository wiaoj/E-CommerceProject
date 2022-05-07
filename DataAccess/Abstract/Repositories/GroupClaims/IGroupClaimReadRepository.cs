using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Entities.Dtos;

namespace DataAccess.Abstract.Repositories.GroupClaims {
	public interface IGroupClaimReadRepository : IEntityReadRepository<GroupOperationClaim> {
		Task<IQueryable<SelectionItem>> GetGroupClaimsSelectedList(Guid id);
	}
}