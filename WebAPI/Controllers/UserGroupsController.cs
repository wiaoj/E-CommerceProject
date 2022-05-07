using Business.Handlers.UserGroups.Commands;
using Business.Handlers.UserGroups.Queries;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers {
	/// <summary>
	/// If controller methods will not be Authorize, [AllowAnonymous] is used.
	/// </summary>
	///
	[Route("api/[controller]")]
	[ApiController]
	public class UserGroupsController : ApiBaseController {
		/// <summary>
		/// List UserGroup
		/// </summary>
		/// <remarks>bla bla bla UserGroups</remarks>
		/// <return>User Grup List</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserGroup>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getAll")]
		public async Task<IActionResult> GetList() {
			return this.GetResponseOnlyResultData(await this.Mediator.Send(new GetUserGroupsQuery()));
		}

		/// <summary>
		/// It brings the details according to its id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SelectionItem>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getByUserId")]
		[AllowAnonymous]
		public async Task<IActionResult> GetByUserId(Guid id) {
			return this.GetResponseOnlyResultData(await this.Mediator.Send(new GetUserGroupLookupQuery { Id = id }));
		}

		/// <summary>
		/// It brings the details according to its id.
		/// </summary>
		/// <remarks>bla bla bla </remarks>
		/// <return>UserGroups List</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserGroup>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getUserGroupByUserId")]
		public async Task<IActionResult> GetGroupClaimsByUserId(Guid id) {
			return this.GetResponseOnlyResultData(await this.Mediator.Send(new GetUserGroupLookupByUserIdQuery { UserId = id }));
		}

		/// <summary>
		/// It brings the details according to its id.
		/// </summary>
		/// <remarks>bla bla bla </remarks>
		/// <return>UserGroups List</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserGroup>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getUsersInGroupByGroupId")]
		public async Task<IActionResult> GetUsersInGroupByGroupid(Guid id) {
			return this.GetResponseOnlyResultData(await this.Mediator.Send(new GetUsersInGroupLookupByGroupIdQuery { GroupId = id }));
		}

		/// <summary>
		/// Add UserGroup.
		/// </summary>
		/// <param name="createUserGroup"></param>
		/// <returns></returns>
		[Consumes("application/json")]
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreateUserGroupCommand createUserGroup) {
			return this.GetResponseOnlyResultMessage(await this.Mediator.Send(createUserGroup));
		}

		/// <summary>
		/// Update UserGroup.
		/// </summary>
		/// <param name="updateUserGroup"></param>
		/// <returns></returns>
		[Consumes("application/json")]
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateUserGroupCommand updateUserGroup) {
			return this.GetResponseOnlyResultMessage(await this.Mediator.Send(updateUserGroup));
		}

		/// <summary>
		/// Update UserGroup by Id.
		/// </summary>
		/// <param name="updateUserGroup"></param>
		/// <returns></returns>
		[Consumes("application/json")]
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpPut("updateByGroupId")]
		public async Task<IActionResult> UpdateByGroupId([FromBody] UpdateUserGroupByGroupIdCommand updateUserGroup) {
			return this.GetResponseOnlyResultMessage(await this.Mediator.Send(updateUserGroup));
		}

		/// <summary>
		/// Delete UserGroup.
		/// </summary>
		/// <param name="deleteUserGroup"></param>
		/// <returns></returns>
		[Consumes("application/json")]
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] DeleteUserGroupCommand deleteUserGroup) {
			return this.GetResponseOnlyResultMessage(await this.Mediator.Send(deleteUserGroup));
		}
	}
}