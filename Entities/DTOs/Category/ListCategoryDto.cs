using Core.Entities.Abstract;

namespace Entities.DTOs.Category {
	public record ListCategoryDto : IDto {
		public Guid Id { get; set; }
		public String Name { get; set; }
		public Int32 ProductCount { get; set; }
	}
}