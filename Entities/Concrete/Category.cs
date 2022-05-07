using Core.Entities.Abstract;

namespace Entities.Concrete {
	public class Category : EntityBase {
		public String Name { get; set; }
		public ICollection<Product> Products { get; set; }
	}
}