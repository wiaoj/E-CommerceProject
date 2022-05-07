using Core.Entities.Abstract;

namespace Entities.Concrete {
	public class Product : EntityBase {
		//public Product() => this.Images = new HashSet<ProductImage>();

		public String Name { get; set; }
		public Decimal UnitPrice { get; set; }
		public Int16 UnitInStock { get; set; }
		//public String QuantityPerUnit { get; set; }
		public String Description { get; set; }


		public Guid CategoryId { get; set; }
		public Category? Category { get; set; }
		public ICollection<ProductImage> Images { get; set; }
	}
}