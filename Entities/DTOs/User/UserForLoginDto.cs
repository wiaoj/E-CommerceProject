using Core.Entities.Abstract;

namespace Entities.DTOs.User {
	public record UserForLoginDto : IDto {
		public String EmailOrPhoneNumber { get; set; }
		public String Password { get; set; }
	}
}