using Core.Entities.Abstract;

namespace Core.Entities.Concrete {
	public class OperationClaim : EntityBase {
		public String Name { get; set; }
		public String Alias { get; set; } = String.Empty;
		public String Description { get; set; } = String.Empty;

		//[Required]
		//public virtual ICollection<UserClaim> UserClaims { get; set; }
	}
}