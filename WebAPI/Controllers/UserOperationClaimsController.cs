using Business.Handlers.UserOperationClaims.Commands;
using Business.Handlers.UserOperationClaims.Queries;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers {
	/// <summary>
	/// If controller methods will not be Authorize, [AllowAnonymous] is used.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class UserOperationClaimsController : ApiBaseController {
		/// <summary>
		/// List UserClaims
		/// </summary>
		/// <remarks>bla bla bla UserClaims</remarks>
		/// <return>UserClaims List</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserOperationClaim>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getAll")]
		public async Task<IActionResult> GetList() {
			return this.GetResponseOnlyResultData(await this.Mediator.Send(new GetUserOperationClaimsQuery()));
		}

		/// <summary>
		/// Id sine göre detaylarını getirir.
		/// </summary>
		/// <remarks>bla bla bla </remarks>
		/// <return>UserOperationClaims List</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserOperationClaim>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getByUserId")]
		public async Task<IActionResult> GetByUserId(Guid id) {
			return this.GetResponseOnlyResultData(await this.Mediator.Send(new GetUserOperationClaimLookupQuery { UserId = id }));
		}

		/// <summary>
		/// It brings the details according to its id.
		/// </summary>
		/// <remarks>bla bla bla </remarks>
		/// <return>UserOperationClaims List</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SelectionItem>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getOperationClaimByUserId")]
		public async Task<IActionResult> GetOperationClaimByUserId(Guid id) {
			return this.GetResponseOnlyResultData(await this.Mediator.Send(new GetUserOperationClaimLookupByUserIdQuery { UserId = id }));
		}

		/// <summary>
		/// Add GroupClaim.
		/// </summary>
		/// <param name="createUserOperationClaim"></param>
		/// <returns></returns>
		[Consumes("application/json")]
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreateUserOperationClaimCommand createUserOperationClaim) {
			return this.GetResponseOnlyResultMessage(await this.Mediator.Send(createUserOperationClaim));
		}

		/// <summary>
		/// Update GroupClaim.
		/// </summary>
		/// <param name="updateUserOperationClaim"></param>
		/// <returns></returns>
		[Consumes("application/json")]
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateUserOperationClaimCommand updateUserOperationClaim) {
			return this.GetResponseOnlyResultMessage(await this.Mediator.Send(updateUserOperationClaim));
		}

		/// <summary>
		/// Delete GroupClaim.
		/// </summary>
		/// <param name="deleteUserOperationClaim"></param>
		/// <returns></returns>
		[Consumes("application/json")]
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] DeleteUserOperationClaimCommand deleteUserOperationClaim) {
			return this.GetResponseOnlyResultMessage(await this.Mediator.Send(deleteUserOperationClaim));
		}
	}
}