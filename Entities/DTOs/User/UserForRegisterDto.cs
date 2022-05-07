using Core.Entities.Abstract;

namespace Entities.DTOs.User {
	public record UserForRegisterDto : IDto {
		public String FirstName { get; set; }
		public String LastName { get; set; }
		public String PhoneNumber { get; set; }
		public String Email { get; set; }
		public String Password { get; set; }
	}
}