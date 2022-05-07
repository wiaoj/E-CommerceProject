using Core.Entities.Abstract;

namespace Core.Entities.Dtos {
	/// <summary>
	/// Simple selectable lists have been created to return with a single schema through the API.
	/// </summary>
	public class SelectionItem : IDto {
#pragma warning disable CS8618 // Non-nullable property 'Id' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Label' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'ParentId' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public SelectionItem() { }
#pragma warning restore CS8618 // Non-nullable property 'ParentId' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning restore CS8618 // Non-nullable property 'Label' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning restore CS8618 // Non-nullable property 'Id' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'ParentId' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public SelectionItem(dynamic id, String label) {
			this.Id = id;
			this.Label = label;
		}
#pragma warning restore CS8618 // Non-nullable property 'ParentId' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

		public dynamic Id { get; set; }
		public String ParentId { get; set; }
		public String Label { get; set; }
		public Boolean IsDisabled { get; set; }
	}
}