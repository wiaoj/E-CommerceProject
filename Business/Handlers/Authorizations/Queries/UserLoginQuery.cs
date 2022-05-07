using Business.Constants.Messages;
using Business.Handlers.Authorizations.ValidationRules;
using Business.Services.Authentication;
using Core.Aspects.Autofac.Securing;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Error;
using Core.Utilities.Results.Concrete.Success;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract.Repositories.Users;
using MediatR;

namespace Business.Handlers.Authorizations.Queries {
	public class UserLoginQuery : IRequest<IDataResult<AccessToken>> {
#pragma warning disable CS8618 // Non-nullable property 'EmailOrPhoneNumber' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public String EmailOrPhoneNumber { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'EmailOrPhoneNumber' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Password' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public String Password { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Password' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

		public class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, IDataResult<AccessToken>> {
			private readonly IUserReadRepository userReadRepository;
			private readonly IUserWriteRepository userWriteRepository;
			private readonly ITokenHelper tokenHelper;
			private readonly ICacheManager cacheManager;
			private readonly IBusinessRules businessRules;

			public UserLoginQueryHandler(IUserReadRepository userReadRepository,
				IUserWriteRepository userWriteRepository,
				ITokenHelper tokenHelper,
				ICacheManager cacheManager,
				IBusinessRules businessRules) {
				this.userReadRepository = userReadRepository;
				this.userWriteRepository = userWriteRepository;
				this.tokenHelper = tokenHelper;
				this.cacheManager = cacheManager;
				this.businessRules = businessRules;
			}

			[SecuredAspect(Priority = 1)]
			[ValidationAspect(typeof(UserLoginValidator), Priority = 1)]
			public async Task<IDataResult<AccessToken>> Handle(UserLoginQuery request, CancellationToken cancellationToken) {
				//TODO
				var user = await this.userReadRepository.GetSingleAsync(user => (user.Email.Equals(request.EmailOrPhoneNumber) || user.PhoneNumber.Equals(request.EmailOrPhoneNumber)) && user.Status);

				//var user = await UserFound(request.EmailOrPhoneNumber);
				if(user is null) {
					return new ErrorDataResult<AccessToken>(Messages.UserNotFound);
				}

				if(!HashingHelper.VerifyPasswordHash(request.Password, user.PasswordSalt, user.PasswordHash)) {
					return new ErrorDataResult<AccessToken>(Messages.PasswordError);
				}

				var claims = this.userReadRepository.GetClaims(user.Id);

				var accessToken = this.tokenHelper.CreateToken<WiaojToken>(user);
				accessToken.Claims = claims.Select(x => x.Name).ToList();

				user.RefreshToken = accessToken.RefreshToken;
				//user.UpdateContactDate = DateTime.Now;
				this.userWriteRepository.Update(user);
				await this.userWriteRepository.SaveChangesAsync();
				this.cacheManager.Add($"{CacheKeys.UserIdForClaim}={user.Id}", claims.Select(x => x.Name));

				return new SuccessDataResult<AccessToken>(accessToken, Messages.UserLoginSuccessful);
			}

			/// <summary>
			/// If user already exist return false else true
			/// </summary>
			/// <param name="email"></param>
			/// <param name="phoneNumber"></param>
			/// <returns></returns>
			private async Task<IDataResult<User>> UserFound(String emailOrPhoneNumber) {
				var result = this.businessRules.Run(await this.CheckUserOfEmailAdress(emailOrPhoneNumber), await this.CheckUserOfPhoneNumber(emailOrPhoneNumber)) as IDataResult<User>;
				return result is null ?
					new SuccessDataResult<User>() :
					new ErrorDataResult<User>();
			}

			#region Business-Rules
			private async Task<IDataResult<User>> CheckUserOfEmailAdress(String emailAddress) {
				//mail adresi yoksa null oluyor
				var userToCheck = await this.userReadRepository.GetSingleAsync(user => user.Email.Equals(emailAddress) && user.Status);
				return userToCheck is not null ?
					new SuccessDataResult<User>(userToCheck) :
					new ErrorDataResult<User>(Messages.UserNotFound);
			}

			private async Task<IDataResult<User>> CheckUserOfPhoneNumber(String phoneNumber) {
				var userToCheck = await this.userReadRepository.GetSingleAsync(user => user.PhoneNumber.Equals(phoneNumber) && user.Status);
				return userToCheck is not null ?
					new SuccessDataResult<User>(userToCheck) :
					new ErrorDataResult<User>(Messages.UserNotFound);
			}

			//TODO
			private async Task<Boolean> CheckUserStatus(User user) {
				var userStatus = await this.userReadRepository.GetSingleAsync(u => u.Id.Equals(user.Id));
				return userStatus.Status;
			}
			#endregion
		}
	}
}