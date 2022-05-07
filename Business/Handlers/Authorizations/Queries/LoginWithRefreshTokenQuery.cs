using Business.Constants.Messages;
using Core.CrossCuttingConcerns.Caching;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Error;
using Core.Utilities.Results.Concrete.Success;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract.Repositories.Users;
using MediatR;

namespace Business.Handlers.Authorizations.Queries {
	public class LoginWithRefreshTokenQuery : IRequest<IResult> {
		public String RefreshToken { get; set; }

		public class LoginWithRefreshTokenQueryHandler : IRequestHandler<LoginWithRefreshTokenQuery, IResult> {
			private readonly IUserReadRepository userReadRepository;
			private readonly IUserWriteRepository userWriteRepository;
			private readonly ITokenHelper tokenHelper;
			private readonly ICacheManager cacheManager;

			public LoginWithRefreshTokenQueryHandler(IUserReadRepository userReadRepository,
				IUserWriteRepository userWriteRepository,
				ITokenHelper tokenHelper,
				ICacheManager cacheManager) {
				this.userReadRepository = userReadRepository;
				this.userWriteRepository = userWriteRepository;
				this.tokenHelper = tokenHelper;
				this.cacheManager = cacheManager;
			}

			public async Task<IResult> Handle(LoginWithRefreshTokenQuery request, CancellationToken cancellationToken) {
				var userToCheck = await this.userReadRepository.GetByRefreshToken(request.RefreshToken);

				if(userToCheck is null) {
					return new ErrorDataResult<User>(Messages.UserNotFound);
				}


				var claims = this.userReadRepository.GetClaims(userToCheck.Id);
				this.cacheManager.Remove($"{CacheKeys.UserIdForClaim}={userToCheck.Id}");
				this.cacheManager.Add($"{CacheKeys.UserIdForClaim}={userToCheck.Id}", claims.Select(x => x.Name));
				var accessToken = this.tokenHelper.CreateToken<AccessToken>(userToCheck);
				userToCheck.RefreshToken = accessToken.RefreshToken;
				this.userWriteRepository.Update(userToCheck);
				await this.userWriteRepository.SaveChangesAsync();
				return new SuccessDataResult<AccessToken>(accessToken, Messages.UserLoginSuccessful);
			}
		}
	}
}

