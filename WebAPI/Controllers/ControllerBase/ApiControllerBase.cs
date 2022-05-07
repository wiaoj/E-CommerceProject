using Business.Handlers.Categories.Commands;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.Abstract.IResult;

namespace WebAPI.Controllers.Base {
	/// <summary>
	/// ApiControllerBase
	/// </summary>
	public abstract class ApiControllerBase : ControllerBase {
		private IMediator? mediator;
		/// <summary>
		/// It is for getting the Mediator instance creation process from the base controller.
		/// </summary>
		private protected IMediator Mediator => this.mediator ??= this.HttpContext.RequestServices.GetService<IMediator>()!;

		/// <summary>
		/// Create
		/// </summary>
		/// <param name="result"></param>
		/// <returns></returns>
		[ApiExplorerSettings(IgnoreApi = true)]
		[NonAction]
		private protected IActionResult Create(IResult result) {
			return result.IsSucceed ?
				StatusCode(StatusCodes.Status201Created, result) :
				BadRequest(result);
		}
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="result"></param>
		/// <returns></returns>
		[ApiExplorerSettings(IgnoreApi = true)]
		private protected IActionResult Update(IResult result) {
			return result.IsSucceed ?
				StatusCode(StatusCodes.Status204NoContent, result) :
				BadRequest(result.Message);
		}
		/// <summary>
		/// Delete/Remove
		/// </summary>
		/// <param name="result"></param>
		/// <returns></returns>
		[ApiExplorerSettings(IgnoreApi = true)]
		private protected IActionResult Remove(IResult result) {
			return result.IsSucceed ?
				StatusCode(StatusCodes.Status204NoContent, result) :
				BadRequest(result.Message);
		}
		/// <summary>
		/// Get Response IResult
		/// </summary>
		/// <param name="result">IResult</param>
		/// <returns></returns>
		[ApiExplorerSettings(IgnoreApi = true)]
		public IActionResult GetResponseResult(IResult result) {
			return result.IsSucceed ? this.Ok(result) : this.BadRequest(result);
		}
		/// <summary>
		/// Get Response IResultData
		/// </summary>
		/// <typeparam name="Type">Data Type</typeparam>
		/// <param name="result">IDataResult</param>
		/// <returns></returns>
		public IActionResult GetResponseDataResult<Type>(IDataResult<Type> result) {
			return result.IsSucceed ? this.Ok(result) : this.BadRequest(result);
		}

	}
}