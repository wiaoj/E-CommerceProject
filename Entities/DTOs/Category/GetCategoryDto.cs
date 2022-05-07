using Core.Entities.Abstract;
using Entities.DTOs.Product;

namespace Entities.DTOs.Category {
	public record GetCategoryDto : IDto {
		public Guid Id { get; set; }
		public String Name { get; set; }
		public ICollection<ListProductDto> Products { get; set; }
	}
}