using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Encyption {
	public class SigningCredentialsHelper {
		//JWT oluşturulması için creadential (sisteme girmek için elimizde olanlar)
		public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey) {
			//key olarak securityKey algoritma olarak HmacSha512Signature kullan dedik
			return new SigningCredentials(
				key: securityKey,
				algorithm: SecurityAlgorithms.HmacSha512Signature);
		}
	}
}