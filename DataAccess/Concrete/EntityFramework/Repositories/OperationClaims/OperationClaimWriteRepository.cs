using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract.Repositories.OperationClaims;

namespace DataAccess.Concrete.EntityFramework.Repositories.OperationClaims {
	public class OperationClaimWriteRepository : EfEntityWriteRepositoryBase<OperationClaim, DataBaseContext>, IOperationClaimWriteRepository {
		public OperationClaimWriteRepository(DataBaseContext context) : base(context) { }
	}
}