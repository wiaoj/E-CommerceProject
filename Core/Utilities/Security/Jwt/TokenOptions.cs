namespace Core.Utilities.Security.Jwt {
	public class TokenOptions {
		public const String ConfigurationName = "TokenOptions";
		public String Audience { get; set; }
		public String Issuer { get; set; }
		public Int32 AccessTokenExpiration { get; set; }
		public String SecurityKey { get; set; }
	}
}