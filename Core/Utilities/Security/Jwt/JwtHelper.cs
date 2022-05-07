using Core.Entities.Concrete;
using Core.Extensions.Claims;
using Core.Utilities.Security.Encyption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Core.Utilities.Security.Jwt {
	public class JwtHelper : ITokenHelper {
		public IConfiguration Configuration { get; } //appsettings dosyasını okumamıza yarıyor
		private readonly TokenOptions _tokenOptions;
		private DateTime _accessTokenExpiration;
		public JwtHelper() { }
		public JwtHelper(IConfiguration configuration) {
			this.Configuration = configuration;
			this._tokenOptions = this.Configuration.GetSection(TokenOptions.ConfigurationName).Get<TokenOptions>(); //Microsoft.Extensions.Configuration.Binder ile hata çözüldü -> nuget install
																													//Configuration => appsettings.json demek
																													//appsetting tokenoptions bölümünü al ve get<> sınıfı içindeki değerlerle maple (doldur)
		}

		public TypeAccessToken CreateToken<TypeAccessToken>(User user) where TypeAccessToken : IAccessToken, new() {
			this._accessTokenExpiration = DateTime.Now.AddMinutes(this._tokenOptions.AccessTokenExpiration); //token süresini alıp şimdiki zamana ekle
			SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(this._tokenOptions.SecurityKey); //securitykeyhelper in createsecuritykey metodu ile key oluştur
			SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey); //hangi algoritma ve anahtarı istiyor yazdığımız method ile alıyoruz değerini
			JwtSecurityToken jwtSecurityToken = this.CreateJwtSecurityToken(this._tokenOptions, user, signingCredentials); //jwttoken oluşturma metodumuz var
			JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
			String token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

			return new() {
				Token = token,
				Expiration = _accessTokenExpiration,
				RefreshToken = this.GenerateRefreshToken()
			};

		}

		public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials) {
			return new(
				issuer: tokenOptions.Issuer,
				audience: tokenOptions.Audience,
				expires: this._accessTokenExpiration,
				notBefore: DateTime.Now,
				claims: SetClaims(user), //claimler önemliymiş ve onun içinde bir metodumuz var
				signingCredentials: signingCredentials);
		}

		public String GenerateRefreshToken() {
			Byte[] randomNumber = new Byte[32];
			using RNGCryptoServiceProvider generator = new();
			generator.GetBytes(randomNumber);
			return Convert.ToBase64String(randomNumber);
		}

		//VS static yap dedi denenecek
		private static IEnumerable<Claim> SetClaims(User user) {
			List<Claim> claims = new();
			claims.AddNameIdentifier(user.Id.ToString()); //.net içinde böyle şeyler yok ve biz Core.Extensions kısmında bu claime yeni metodlar ekliyoruz
			claims.AddEmail(user.Email);                    //ve bu olaya extensions yani genişletme deniliyor
															//claims.Add(new Claim(JwtRegisteredClaimNames.Email, email)); //bu şekilde de ekleyebiliriz
			claims.AddName($"{user.FirstName} {user.LastName}");
			//claims.AddRoles(operationClaims.Select(claims => claims.Name).ToArray());

			claims.Add(new Claim(ClaimTypes.Role, user.AuthenticationProviderType));
			return claims;
		}

		public String DecodeToken(String input) {
			var handler = new JwtSecurityTokenHandler();
			if(input.StartsWith("Bearer ")) {
				input = input["Bearer ".Length..];
			}

			return handler.ReadJwtToken(input).ToString();
		}
	}
}