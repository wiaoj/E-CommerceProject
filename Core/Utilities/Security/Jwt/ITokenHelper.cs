using Core.Entities.Concrete;

namespace Core.Utilities.Security.Jwt;
public interface ITokenHelper {
	TypeAccessToken CreateToken<TypeAccessToken>(User user)
		where TypeAccessToken : IAccessToken, new();
	String GenerateRefreshToken();
}
