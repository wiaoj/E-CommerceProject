using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Results.Concrete.Success;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.Abstract.IResult;

#nullable disable

namespace WebAPI.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	//TODO: API controller 
	public abstract class ApiBaseController : ControllerBase {
		private IMediator mediator;
		/// <summary>
		/// It is for getting the Mediator instance creation process from the base controller.
		/// </summary>
		private protected IMediator Mediator => this.mediator ??= this.HttpContext.RequestServices.GetService<IMediator>();


		[ApiExplorerSettings(IgnoreApi = true)]
		public IActionResult Create(IResult result) {
			return StatusCode(StatusCodes.Status201Created, result);
		}
		
		[ApiExplorerSettings(IgnoreApi = true)]
		public IActionResult Update(IResult result) {
			return StatusCode(StatusCodes.Status204NoContent, result);
		}
		
		[ApiExplorerSettings(IgnoreApi = true)]
		private protected IActionResult Delete(IResult result) {
			return StatusCode(StatusCodes.Status204NoContent, result);
		}

		/// <summary>
		/// response with IDataResult
		/// </summary>
		/// <typeparam name="Type"></typeparam>
		/// <param name="result"></param>
		/// <returns></returns>
		[ApiExplorerSettings(IgnoreApi = true)]
		public IActionResult GetResponse<Type>(IDataResult<Type> result) {
			return result.IsSucceed ? this.Ok(result) : this.BadRequest(result);
		}

		/// <summary>
		/// response only IResult
		/// </summary>
		/// <param name="result"></param>
		/// <returns></returns>
		[ApiExplorerSettings(IgnoreApi = true)]
		public IActionResult GetResponseOnlyResult(IResult result) {
			return result.IsSucceed ? this.Ok(result) : this.BadRequest(result);
		}

		/// <summary>
		/// response only IResult message
		/// </summary>
		/// <param name="result"></param>
		/// <returns></returns>
		[ApiExplorerSettings(IgnoreApi = true)]
		public IActionResult GetResponseOnlyResultMessage(IResult result) {
			return result.IsSucceed ? this.Ok(result) : this.BadRequest(result.Message);
		}

		/// <summary>
		/// response IDataResult data
		/// </summary>
		/// <typeparam name="Type"></typeparam>
		/// <param name="result"></param>
		/// <returns></returns>
		[ApiExplorerSettings(IgnoreApi = true)]
		public IActionResult GetResponseOnlyResultData<Type>(IDataResult<Type> result) {
			return result.IsSucceed ? this.Ok(result) : this.BadRequest(result);
		}

		[NonAction]
		protected IActionResult Unauthorized<T>(String message, String internalMessage, T data) {
			return this.Unauthorized(new ApiResult<T> {
				Success = false,
				Message = message,
				InternalMessage = internalMessage,
				Data = data
			});
		}
		/// <summary>
		///
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <returns></returns>
		[NonAction]
		protected IActionResult Unauthorized<T>(ApiResult<T> data) {
			return this.StatusCode(401, data);
		}

		/// <summary>
		///
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="message"></param>
		/// <param name="internalMessage"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		[NonAction]
		protected IActionResult Error<T>(String message, String internalMessage, T data) {
			return this.Error(new ApiResult<T> {
				Success = false,
				Message = message,
				InternalMessage = internalMessage,
				Data = data
			});
		}

		/// <summary>
		///
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <returns></returns>
		[NonAction]
		protected IActionResult Error<T>(ApiResult<T> data) {
			return this.StatusCode(500, data);
		}
	}
}