using Core.Entities.Abstract;

namespace Core.Entities.Dtos {
	public class UserDto : IDto {
		//public override Guid Id { get => base.Id; set => base.Id = value; }
#pragma warning disable CS8618 // Non-nullable property 'FirstName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public String FirstName { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'FirstName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'LastName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public String LastName { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'LastName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Email' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public String Email { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Email' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'PhoneNumber' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public String PhoneNumber { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'PhoneNumber' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		//public String Address { get; set; }
		public Boolean Gender { get; set; }
		public DateTime BirthDate { get; set; }
		//public Boolean Status { get; set; }
		//public String RefreshToken { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }
	}
}