namespace Core.Utilities.Security.Jwt {
	public interface IAccessToken {
		public String Token { get; set; }
		public DateTime Expiration { get; set; }
		public String RefreshToken { get; set; }
	}
}