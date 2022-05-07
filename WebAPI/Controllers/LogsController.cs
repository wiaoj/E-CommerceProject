using Business.Handlers.Logs.Queries;
using Core.Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class LogsController : ControllerBase {
		private IMediator mediator;

		/// <summary>
		/// It is for getting the Mediator instance creation process from the base controller.
		/// </summary>
		protected IMediator Mediator => this.mediator ??= this.HttpContext.RequestServices.GetService<IMediator>();
		/// <summary>
		/// List Logs
		/// </summary>
		/// <remarks>bla bla bla Logs</remarks>
		/// <return>Logs List</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LogDto>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getAll")]
		public async Task<IActionResult> GetList() {
			var result = await this.Mediator.Send(new GetLogDtoQuery());
			return result.IsSucceed ? this.Ok(result.Data) : this.BadRequest(result.Message);
		}
	}
}