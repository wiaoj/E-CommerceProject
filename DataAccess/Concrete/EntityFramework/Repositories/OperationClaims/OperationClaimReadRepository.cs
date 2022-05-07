using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract.Repositories.OperationClaims;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac {
	public class OperationClaimReadRepository : EfEntityReadRepositoryBase<OperationClaim, DataBaseContext>, IOperationClaimReadRepository {
		public OperationClaimReadRepository(DataBaseContext context) : base(context) { }
	}
}