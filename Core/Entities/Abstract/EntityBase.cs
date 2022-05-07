namespace Core.Entities.Abstract {
	public abstract class EntityBase : IEntity<Guid, DateTime> {
		public Guid Id { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }
	}
}