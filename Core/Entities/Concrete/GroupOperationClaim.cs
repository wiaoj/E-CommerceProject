using Core.Entities.Abstract;

namespace Core.Entities.Concrete {
	public class GroupOperationClaim : EntityBase {
		public Guid GroupId { get; set; }
		public Guid OperationClaimId { get; set; }
		//public virtual ICollection<OperationClaim> OperationClaims { get; set; }
	}
}