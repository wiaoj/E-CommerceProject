using Business.Constants.Messages;
using Business.Handlers.Authorizations.ValidationRules;
using Core.Aspects.Autofac.Caching;
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
	public class UserRegisterQuery : IRequest<IResult> {
#pragma warning disable CS8618 // Non-nullable property 'FirstName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public String FirstName { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'FirstName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'LastName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public String LastName { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'LastName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Email' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public String Email { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Email' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'PhoneNumber' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public String PhoneNumber { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'PhoneNumber' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Password' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public String Password { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Password' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

		public class UserRegisterQueryHandler : IRequestHandler<UserRegisterQuery, IResult> {
			private readonly IUserReadRepository userReadRepository;
			private readonly IUserWriteRepository userWriteRepository;
			private readonly ITokenHelper tokenHelper;
			private readonly ICacheManager cacheManager;
			private readonly IBusinessRules businessRules;

			public UserRegisterQueryHandler(IUserReadRepository userReadRepository,
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

			//[SecuredAspect(Priority = 1)]
			[ValidationAspect(typeof(UserRegisterValidator), Priority = 2)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(UserRegisterQuery request, CancellationToken cancellationToken) {
				//var isThereAnyUser = await this.userRepository.GetAsync(u => u.Email.Equals(request.Email) || u.PhoneNumber.Equals(request.PhoneNumber));

				var isThereAnyUser = await this.IsThereAnyUser(request.Email, request.PhoneNumber);

				if(isThereAnyUser.IsSucceed.Equals(default)) {
					return new ErrorResult(isThereAnyUser.Message);
				}

				HashingHelper.CreatePasswordHash(request.Password, out Byte[] passwordSalt, out Byte[] passwordHash);
				User user = new() {
					FirstName = request.FirstName,
					LastName = request.LastName,
					Email = request.Email,
					PhoneNumber = request.PhoneNumber,
					PasswordHash = passwordHash,
					PasswordSalt = passwordSalt,
					RefreshToken = this.tokenHelper.GenerateRefreshToken(),
					Status = true
				};

				await this.userWriteRepository.AddAsync(user);
				await this.userWriteRepository.SaveChangesAsync();
				return new SuccessResult(Messages.UserRegisterSuccessful);
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="email"></param>
			/// <param name="phoneNumber"></param>
			/// <returns>If user already exist return false else true</returns>
			private async Task<IResult> IsThereAnyUser(String email, String phoneNumber) {
				return this.businessRules.Run(await this.CheckUserOfEmailAdress(email), await this.CheckUserOfPhoneNumber(phoneNumber)) as IDataResult<User> is null ?
					new SuccessResult() :
					new ErrorResult(Messages.UserAlredyExists);
			}

			#region Business-Rules
			private async Task<IDataResult<User>> CheckUserOfEmailAdress(String emailAddress) {
				//mail adresi yoksa null oluyor
				var userToCheck = await this.userReadRepository.GetSingleAsync(u => u.Email.Equals(emailAddress));
				return userToCheck is null ?
					new SuccessDataResult<User>() :
					new ErrorDataResult<User>(Messages.UserOfEmailAddressAlredyExists);
			}

			private async Task<IDataResult<User>> CheckUserOfPhoneNumber(String phoneNumber) {
				var userToCheck = await this.userReadRepository.GetSingleAsync(u => u.PhoneNumber.Equals(phoneNumber));
				return userToCheck is null ?
					new SuccessDataResult<User>() :
					new ErrorDataResult<User>(Messages.UserOfPhoneNumberAlredyExists);
			}
			#endregion
		}
	}
}