using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Core.Utilities.Security.Encyption {
	public class SecurityKeyHelper {
		//appsettings içerisine yazdığımız securityKeyi ekliyoruz
		public static SecurityKey CreateSecurityKey(String securityKey) {
			//return new AsymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
			return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
		}
	}
}