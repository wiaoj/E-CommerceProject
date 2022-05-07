using Business.Handlers.Groups.Commands;
using Business.Handlers.Groups.Queries;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers {
	/// <summary>
	/// If controller methods will not be Authorize, [AllowAnonymous] is used.
	/// </summary>
	///
	[Route("api/[controller]")]
	[ApiController]
	public class GroupsController : ApiBaseController {
		/// <summary>
		/// List Groups
		/// </summary>
		/// <remarks>bla bla bla Groups</remarks>
		/// <return>Grup List</return>
		/// <response code="200"></response>
		// [AllowAnonymous]
		// [Produces("application/json","text/plain")]
		// [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Group>))]
		// [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[HttpGet("getAll")]
		public async Task<IActionResult> GetList() {
			return this.GetResponseOnlyResultData(await this.Mediator.Send(new GetGroupsQuery()));
		}

		/// <summary>
		/// It brings the details according to its id.
		/// </summary>
		/// <remarks>bla bla bla </remarks>
		/// <return>Grup List</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Group))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getById")]
		public async Task<IActionResult> GetById(Guid id) {
			return this.GetResponseOnlyResultData(await this.Mediator.Send(new GetGroupQuery { Id = id }));
		}

		/// <summary>
		/// Group Lookup
		/// </summary>
		/// <remarks>Group Lookup döner </remarks>
		/// <return>Grup Lokup</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SelectionItem>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getGroupLookup")]
		public async Task<IActionResult> GetSelectedlist() {
			return this.GetResponseOnlyResultData(await this.Mediator.Send(new GetGroupLookupQuery()));
		}

		/// <summary>
		/// Add Group
		/// </summary>
		/// <param name="createGroup"></param>
		/// <returns></returns>
		[Consumes("application/json")]
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreateGroupCommand createGroup) {
			return this.GetResponseOnlyResultMessage(await this.Mediator.Send(createGroup));
		}

		/// <summary>
		/// Update Group
		/// </summary>
		/// <param name="updateGroup"></param>
		/// <returns></returns>
		[Consumes("application/json")]
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateGroupCommand updateGroup) {
			return this.GetResponseOnlyResultMessage(await this.Mediator.Send(updateGroup));
		}

		/// <summary>
		/// Delete Group
		/// </summary>
		/// <param name="deleteGroup"></param>
		/// <returns></returns>
		[Consumes("application/json")]
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] DeleteGroupCommand deleteGroup) {
			return this.GetResponseOnlyResultMessage(await this.Mediator.Send(deleteGroup));
		}
	}
}