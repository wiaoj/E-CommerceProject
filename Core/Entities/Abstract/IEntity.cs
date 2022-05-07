namespace Core.Entities.Abstract {
	public interface IEntityBase { }
	public interface IEntityBase<out TypeId, out TypeDate> : IEntityBase { }
	public interface IEntity<TypeId, TypeDate> : IEntityBase<TypeId, TypeDate>
		where TypeId : struct
		where TypeDate : struct {
		public TypeId Id { get; set; }
		public TypeDate CreatedDate { get; set; }
		public TypeDate UpdatedDate { get; set; } 
	}
}