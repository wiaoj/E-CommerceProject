using Core.Entities.Abstract;

namespace Core.Entities.Dtos {
	public class LogDto : EntityBase {
#pragma warning disable CS8618 // Non-nullable property 'Level' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public String Level { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Level' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'ExceptionMessage' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public String ExceptionMessage { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'ExceptionMessage' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public DateTime TimeStamp { get; set; }
#pragma warning disable CS8618 // Non-nullable property 'User' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public String User { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'User' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Value' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public String Value { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Value' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Type' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public String Type { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Type' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
	}
}