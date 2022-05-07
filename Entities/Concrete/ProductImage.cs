using Core.Entities.Abstract;

namespace Entities.Concrete {
	public class ProductImage : EntityBase {
		public String ImagePath { get; set; }
		public Guid ProductId { get; set; }
		public Product Product { get; set; }
	}
}