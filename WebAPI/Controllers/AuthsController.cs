using Business.Handlers.Authorizations.Commands;
using Business.Handlers.Authorizations.Queries;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.Abstract.IResult;

namespace WebAPI.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	[ApiConventionType(typeof(DefaultApiConventions))]
	public class AuthsController : ApiBaseController {

		/// <summary>
		/// User Login operations
		/// </summary>
		/// <remarks>Sample request: </remarks>
		/// <param name="loginModel"></param>
		/// <returns>returns</returns>
		[AllowAnonymous]
		[Consumes("application/json")]
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<AccessToken>))]
		[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(String))]
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] UserLoginQuery loginModel) {
			var result = await this.Mediator.Send(loginModel);
			return result.IsSucceed ? this.Ok(result) : this.Unauthorized(result.Message);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		[AllowAnonymous]
		[Consumes("application/json")]
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<AccessToken>))]
		[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(String))]
		[HttpPost("loginWithRefreshToken")]
		public async Task<IActionResult> LoginWithRefreshToken([FromBody] LoginWithRefreshTokenQuery command) {
			return this.GetResponseOnlyResult(await this.Mediator.Send(command));
		}

		/// <summary>
		/// User Register operations
		/// </summary>
		/// <remarks>Sample request: </remarks>
		/// <param name="createUser"></param>
		/// <returns></returns>
		[AllowAnonymous]
		[Consumes("application/json")]
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] UserRegisterQuery createUser) {
			//var result = await Mediator.Send(createUser);
			return this.GetResponseOnlyResult(await this.Mediator.Send(createUser));
			//return result.Success ? Ok(result) : BadRequest(result);
		}

		/// <summary>
		/// Make it Forgot Password operations
		/// </summary>
		/// <remarks>tckimlikno</remarks>
		/// <return></return>
		/// <response code="200"></response>
		[AllowAnonymous]
		[Consumes("application/json")]
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
		[HttpPut("forgotPassword")]
		public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand forgotPassword) {
			return this.GetResponseOnlyResult(await this.Mediator.Send(forgotPassword));
		}

		/// <summary>
		/// Make it Change Password operation
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		[Consumes("application/json")]
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpPut("changeUserPassword")]
		public async Task<IActionResult> ChangeUserPassword([FromBody] ChangePasswordCommand command) {
			return this.GetResponseOnlyResultMessage(await this.Mediator.Send(command));
		}

		/// <summary>
		/// Token decode test
		/// </summary>
		/// <returns></returns>
		[Consumes("application/json")]
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
		[HttpPost("test")]
		public IActionResult LoginTest() {
			var auth = this.Request.Headers["Authorization"];
			return this.Ok(new JwtHelper().DecodeToken(auth));
		}
	}
}