using Core.Entities.Abstract;

namespace Entities.Concrete {
	public class Customer : EntityBase {
		public Guid UserId { get; set; }
		public Guid AdressId { get; set; }
		public String PhoneNumber { get; set; }
	}
}