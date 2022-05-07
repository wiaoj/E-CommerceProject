namespace Core.Utilities.Security.Jwt {
	public class AccessToken : IAccessToken {
		public List<String> Claims { get; set; }
		public String Token { get; set; }
		public DateTime Expiration { get; set; }
		public String RefreshToken { get; set; }
	}
}