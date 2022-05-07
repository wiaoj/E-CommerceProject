using Business.Handlers.Products.Commands;
using Business.Handlers.Products.Queries;
using Core.Entities.Dtos;
using Entities.Concrete;
using Entities.DTOs.Product;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ApiControllerBase {
		/// <summary>
		/// List Products
		/// </summary>
		/// <remarks>bla bla bla Products</remarks>
		/// <return>Products List</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetProductDto>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getDetailsAll")]
		public async Task<IActionResult> GetDetailsList() {
			return this.GetResponseDataResult(await this.Mediator.Send(new GetProductsDetailsQuery()));
		}

		/// <summary>
		/// Get Product Details By Id
		/// </summary>
		/// <remarks>bla bla bla Product</remarks>
		/// <return>Product List</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProductDto))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getDetailsById")]
		public async Task<IActionResult> GetDetailsById([FromQuery] Guid id) {
			return this.GetResponseDataResult(await this.Mediator.Send(new GetProductDetailsQuery() { Id = id }));
		}

		/// <summary>
		/// Product Lookup
		/// </summary>
		/// <remarks>Get Lookup</remarks>
		/// <return>Get Lokup</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SelectionItem>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getLookup")]
		public async Task<IActionResult> GetSelectedlist() {
			return this.GetResponseDataResult(await this.Mediator.Send(new GetProductsLookupQuery()));
		}

		/// <summary>
		/// Add Products
		/// </summary>
		/// <remarks>bla bla bla Products</remarks>
		/// <return>Products Add</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreateProductCommand command) {
			return this.Create(await this.Mediator.Send(command));
		}

		/// <summary>
		/// Update Products
		/// </summary>
		/// <remarks>bla bla bla Products</remarks>
		/// <return>Products Update</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateProductCommand command) {
			return this.Update(await this.Mediator.Send(command));
		}

		/// <summary>
		/// Delete Products
		/// </summary>
		/// <remarks>bla bla bla Products</remarks>
		/// <return>Products Delete</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] DeleteProductCommand command) {
			return this.Remove(await this.Mediator.Send(command));
		}
	}
}
