using Business.Handlers.Users.Commands;
using Business.Handlers.Users.Queries;
using Core.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers {
	/// <summary>
	/// If controller methods will not be Authorize, [AllowAnonymous] is used.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ApiBaseController {
		/// <summary>
		/// List Users
		/// </summary>
		/// <remarks>bla bla bla Users</remarks>
		/// <return>Users List</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserDto>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getAll")]
		public async Task<IActionResult> GetList() {
			return this.GetResponseOnlyResultData(await this.Mediator.Send(new GetUsersQuery()));
		}

		/// <summary>
		/// User Lookup
		/// </summary>
		/// <remarks>bla bla bla Users</remarks>
		/// <return>Users List</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SelectionItem>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getUserLookup")]
		public async Task<IActionResult> GetUserLookup() {
			return this.GetResponseOnlyResultData(await this.Mediator.Send(new GetUserLookupQuery()));
		}

		/// <summary>
		/// It brings the details according to its id.
		/// </summary>
		/// <remarks>bla bla bla </remarks>
		/// <return>Users List</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getById")]
		public async Task<IActionResult> GetById(Guid id) {
			return this.GetResponseOnlyResultData(await this.Mediator.Send(new GetUserQuery { Id = id }));
		}

		/// <summary>
		/// Add User
		/// </summary>
		/// <param name="createUser"></param>
		/// <returns></returns>
		[Consumes("application/json")]
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreateUserCommand createUser) {
			return this.GetResponseOnlyResultMessage(await this.Mediator.Send(createUser));
		}

		/// <summary>
		/// Update User
		/// </summary>
		/// <param name="updateUser"></param>
		/// <returns></returns>
		[Consumes("application/json")]
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateUserCommand updateUser) {
			return this.GetResponseOnlyResultMessage(await this.Mediator.Send(updateUser));
		}

		/// <summary>
		/// Delete User
		/// </summary>
		/// <param name="deleteUser"></param>
		/// <returns></returns>
		[Consumes("application/json")]
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] DeleteUserCommand deleteUser) {
			return this.GetResponseOnlyResultMessage(await this.Mediator.Send(deleteUser));
		}
	}
}