namespace Business.Services.Authentication.Models {
	/// <summary>
	/// It is the return data of the login function.
	/// </summary>
	public class UserLoginResult {
		public enum LoginStatus {
			UserNotFound,
			//WrongCredentials,
			PhoneNumberRequired,
			ServiceError,
			Ok
		}

		/// <summary>
		/// Login query result.
		/// </summary>
		public LoginStatus Status { get; set; }

		/// <summary>
		/// Additional message
		/// </summary>
#pragma warning disable CS8618 // Non-nullable property 'Message' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public String Message { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Message' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

		/// <summary>
		/// List of registered phone numbers for users in the system.
		/// </summary>
#pragma warning disable CS8618 // Non-nullable property 'MobilePhones' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public String[] MobilePhones { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'MobilePhones' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
	}
}