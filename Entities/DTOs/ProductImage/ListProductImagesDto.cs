using Core.Entities.Abstract;

namespace Entities.DTOs.ProductImage {
	public record ListProductImagesDto : IDto {
		public Guid Id { get; set; }
		public Guid ProductId { get; set; }
		public String ImagePath { get; set; }
	}
}