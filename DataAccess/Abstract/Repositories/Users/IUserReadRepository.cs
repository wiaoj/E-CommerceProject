using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract.Repositories.Users {
	public interface IUserReadRepository : IEntityReadRepository<User> {
		IQueryable<OperationClaim> GetClaims(Guid id);
		Task<User?> GetByRefreshToken(String refreshToken);
	}
}