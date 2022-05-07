using Business.Handlers.ProductImages.Commands;
using Business.Handlers.ProductImages.Queries;
using Entities.Concrete;
using Entities.DTOs.ProductImage;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class ProductImagesController : ApiBaseController {
		/// <summary>
		/// List Products Images
		/// </summary>
		/// <remarks>bla bla bla Products Images</remarks>
		/// <return>Products Images List</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductImage))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getAll")]
		public async Task<IActionResult> GetList() {
			return this.GetResponseOnlyResultData(await this.Mediator.Send(new GetProductImagesQuery()));
		}

		/// <summary>
		/// List Products Images Details
		/// </summary>
		/// <remarks>bla bla bla Products Images</remarks>
		/// <return>Products Images Details List</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ListProductImagesDto>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getDetailsAll")]
		public async Task<IActionResult> GetDetailsList() {
			return this.GetResponseOnlyResultData(await this.Mediator.Send(new GetProductsImagesDetailsQuery()));
		}

		/// <summary>
		/// GetByProductId Images
		/// </summary>
		/// <param name="productId"></param>
		/// <returns></returns>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductImage))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getByProductId")]
		public async Task<IActionResult> GetByProductId([FromQuery] Guid productId) {
			return this.GetResponseOnlyResultData(await this.Mediator.Send(new GetProductImageQuery() { ProductId = productId }));
		}

		/// <summary>
		/// GetByProductId Images Details
		/// </summary>
		/// <param name="productId"></param>
		/// <returns></returns>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProductImageDto))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getDetailsByProductId")]
		public async Task<IActionResult> GetDetailsByProductId([FromQuery] Guid productId) {
			return this.GetResponseOnlyResultData(await this.Mediator.Send(new GetProductImageDetailsQuery() { ProductId = productId }));
		}
		/// <summary>
		/// Add Product Images
		/// </summary>
		/// <remarks>bla bla bla Product Images</remarks>
		/// <return>Product Images Add</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpPost]
		public async Task<IActionResult> Add([FromForm] CreateProductImageCommand command) {
			return this.GetResponseOnlyResultMessage(await this.Mediator.Send(command));
		}

		/// <summary>
		/// Update Product Images
		/// </summary>
		/// <remarks>bla bla bla Product Images</remarks>
		/// <return>Product Images Update</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpPut]
		public async Task<IActionResult> Update([FromForm] UpdateProductImageCommand command) {
			return this.GetResponseOnlyResultMessage(await this.Mediator.Send(command));
		}

		/// <summary>
		/// Delete Product Images
		/// </summary>
		/// <remarks>bla bla bla Product Images</remarks>
		/// <return>Product Images Delete</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpDelete]
		public async Task<IActionResult> Delete([FromForm] DeleteProductImageCommand command) {
			return this.GetResponseOnlyResultMessage(await this.Mediator.Send(command));
		}
	}
}
