using Core.Entities.Abstract;

namespace Core.Entities.Concrete {
	public class UserOperationClaim : EntityBase {
		//[Required]
		public Guid UserId { get; set; }

		//[Required]
		public Guid OperationClaimId { get; set; }
		//public virtual ICollection<OperationClaim> OperationClaims { get; set; }
	}
}