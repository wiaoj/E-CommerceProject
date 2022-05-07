using Core.Entities.Abstract;

namespace Entities.DTOs.ProductImage {
	public record GetProductImageDto : IDto {
		public Guid Id { get; set; }
		public DateTime UpdatedDate { get; set; }
		public Guid ProductId { get; set; }
		public String ImagePath { get; set; }
	}
}