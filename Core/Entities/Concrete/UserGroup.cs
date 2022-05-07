using Core.Entities.Abstract;

namespace Core.Entities.Concrete {
	public class UserGroup : EntityBase {
		public Guid GroupId { get; set; }
		public Guid UserId { get; set; }
		//public virtual User Users { get; set; }
	}
}