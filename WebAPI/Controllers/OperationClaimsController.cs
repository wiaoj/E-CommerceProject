using Business.Handlers.OperationClaims.Commands;
using Business.Handlers.OperationClaims.Queries;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.Abstract.IResult;

namespace WebAPI.Controllers {
	/// <summary>
	/// If controller methods will not be Authorize, [AllowAnonymous] is used.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class OperationClaimsController : ApiBaseController {
		/// <summary>
		/// List OperationClaims
		/// </summary>
		/// <remarks>bla bla bla OperationClaims</remarks>
		/// <return>OperationClaims List</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OperationClaim>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getAll")]
		public async Task<IActionResult> GetList() {
			return this.GetResponseOnlyResultData(await this.Mediator.Send(new GetOperationClaimsQuery()));
		}

		/// <summary>
		/// It brings the details according to its id.
		/// </summary>
		/// <remarks>bla bla bla OperationClaims</remarks>
		/// <return>OperationClaims List</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OperationClaim))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getById")]
		public async Task<IActionResult> GetByid(Guid id) {
			return this.GetResponseOnlyResultData(await this.Mediator.Send(new GetOperationClaimQuery() { Id = id }));

			//await Mediator.Send(new CreateOperationClaimCommand());
			//return Ok(id);
		}

		/// <summary>
		/// List OperationClaims
		/// </summary>
		/// <remarks>bla bla bla OperationClaims</remarks>
		/// <return>OperationClaims List</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SelectionItem>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getOperationClaimsLookup")]
		public async Task<IActionResult> GetOperationClaimsLookup() {
			return this.GetResponseOnlyResultData(await this.Mediator.Send(new GetOperationClaimsLookupQuery()));
		}

		/// <summary>
		/// Create Operation Claims
		/// </summary>
		/// <remarks> Create Operation Claims but only if they are not in the database </remarks>
		/// <return>Create Operation Claims</return>
		/// <response code="200"></response>
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
		[HttpPost("createClaims")]
		public async Task<IActionResult> CreateClaims() {
			return this.Ok(await this.Mediator.Send(new CreateOperationClaimCommand()));
		}

		/// <summary>
		/// Update OperationClaim .
		/// </summary>
		/// <param name="updateOperationClaim"></param>
		/// <returns></returns>
		[Consumes("application/json")]
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpPut("update")]
		public async Task<IActionResult> Update([FromBody] UpdateOperationClaimCommand updateOperationClaim) {
			return this.GetResponseOnlyResultMessage(await this.Mediator.Send(updateOperationClaim));
		}

		/// <summary>
		/// List OperationClaims
		/// </summary>
		/// <remarks>bla bla bla OperationClaims</remarks>
		/// <return>OperationClaims List</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OperationClaim>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getUserOperationClaimsFromCache")]
		public async Task<IActionResult> GetUserClaimsFromCache() {
			return this.GetResponseOnlyResultData(await this.Mediator.Send(new GetUserOperationClaimsFromCacheQuery()));
		}
	}
}