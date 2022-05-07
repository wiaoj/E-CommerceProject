using Core.Entities.Concrete;
using Core.Utilities.Security.Jwt;

namespace Business.Services.Authentication {
	public class WiaojToken : AccessToken {
#pragma warning disable CS8618 // Non-nullable property 'ExternalUserId' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public String ExternalUserId { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'ExternalUserId' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public AuthenticationProviderType ProviderType { get; set; }
#pragma warning disable CS8618 // Non-nullable property 'OnBehalfOf' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public String OnBehalfOf { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'OnBehalfOf' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
	}
}