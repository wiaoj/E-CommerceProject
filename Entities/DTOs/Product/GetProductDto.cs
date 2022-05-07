using Core.Entities.Abstract;
using Entities.DTOs.ProductImage;

namespace Entities.DTOs.Product {
	public record GetProductDto : IDto {
		public Guid Id { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }
		public String Name { get; set; }
		public String CategoryName { get; set; }
		public Decimal UnitPrice { get; set; }
		public Int16 UnitInStock { get; set; }
		public String Description { get; set; }
		public ICollection<GetProductImageDto> Images { get; set; }
	}
}