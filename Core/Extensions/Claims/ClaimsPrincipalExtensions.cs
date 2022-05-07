using System.Security.Claims;

#nullable enable

namespace Core.Extensions.Claims {
	public static class ClaimsPrincipalExtensions {
		//tokeni gelen kişinin claimlerini okumak için kullanıyoruz
		public static List<String>? Claims(this ClaimsPrincipal claimsPrincipal, String claimType) {
			var result = claimsPrincipal?.FindAll(claimType)?.Select(claim => claim.Value).ToList();
			return result;
		}

		public static List<String>? ClaimRoles(this ClaimsPrincipal claimsPrincipal) {
			return claimsPrincipal?.Claims(ClaimTypes.Role);
		}
	}
}