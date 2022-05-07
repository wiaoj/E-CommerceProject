using Business.Handlers.GroupsOperationClaims.Commands;
using Business.Handlers.GroupsOperationClaims.Queries;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers {
	/// <summary>
	///
	/// </summary>
	///
	[Route("api/[controller]")]
	[ApiController]
	public class GroupOperationClaimsController : ApiBaseController {
		/// <summary>
		/// GroupOperationClaims list
		/// </summary>
		/// <remarks>GroupOperationClaims</remarks>
		/// <return>GroupOperationClaims List</return>
		/// <response code="200"></response>
		// [AllowAnonymous]
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GroupOperationClaim>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getAll")]
		public async Task<IActionResult> GetList() {
			return this.GetResponseOnlyResultMessage(await this.Mediator.Send(new GetGroupOperationClaimsQuery()));
		}

		/// <summary>
		/// It brings the details according to its id.
		/// </summary>
		/// <remarks>bla bla bla </remarks>
		/// <return>GroupOperationClaims List</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GroupOperationClaim))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getById")]
		public async Task<IActionResult> GetById(Guid id) {
			return this.GetResponseOnlyResultMessage(await this.Mediator.Send(new GetGroupOperationClaimQuery { Id = id }));
		}

		/// <summary>
		/// Brings up OperationClaims by Group Id.
		/// </summary>
		/// <remarks>bla bla bla </remarks>
		/// <return>GroupOperationClaims List</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SelectionItem>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getGroupOperationClaimsByGroupId")]
		public async Task<IActionResult> GetGroupOperationClaimsByGroupId(Guid id) {
			return this.GetResponseOnlyResultData(
				await this.Mediator.Send(new GetGroupOperationClaimsLookupByGroupIdQuery { GroupId = id }));
		}

		/// <summary>
		/// Add GroupOperationClaim .
		/// </summary>
		/// <param name="createGroupOperationClaim"></param>
		/// <returns></returns>
		[Consumes("application/json")]
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreateGroupOperationClaimCommand createGroupOperationClaim) {
			return this.GetResponseOnlyResultMessage(await this.Mediator.Send(createGroupOperationClaim));
		}

		/// <summary>
		/// Update GroupOperationClaim.
		/// </summary>
		/// <param name="updateGroupOperationClaim"></param>
		/// <returns></returns>
		[Consumes("application/json")]
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateGroupOperationClaimCommand updateGroupOperationClaim) {
			return this.GetResponseOnlyResultMessage(await this.Mediator.Send(updateGroupOperationClaim));
		}

		/// <summary>
		/// Delete GroupOperationClaim.
		/// </summary>
		/// <param name="deleteGroupOperationClaim"></param>
		/// <returns></returns>
		[Consumes("application/json")]
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] DeleteGroupOperationClaimCommand deleteGroupOperationClaim) {
			return this.GetResponseOnlyResultMessage(await this.Mediator.Send(deleteGroupOperationClaim));
		}
	}
}