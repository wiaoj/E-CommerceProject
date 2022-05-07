using Business.Handlers.Categories.Commands;
using Business.Handlers.Categories.Queries;
using Core.Utilities.Pagination;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs.Category;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Base;
using IResult = Core.Utilities.Results.Abstract.IResult;

namespace WebAPI.Controllers {
	/// <summary>
	/// Categories API
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ApiControllerBase {
		/// <summary>
		/// List Categories
		/// </summary>
		/// <remarks>bla bla bla Categories</remarks>
		/// <return>Categories List</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IQueryable<Category>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpPost("getAll")]
		public async Task<IActionResult> GetList(/*[FromQuery] Pagination pagination*/) {

			//List<Category> category = new();
			//for(int i = 1; i <= 1_000_000; i++) {
			//	category.Add(new() {
			//		Name = $"Deneme {i}"
			//	}
			//		);
			//}
			//await this.categoryWriteRepository.AddRangeAsync(category);
			//await this.categoryWriteRepository.SaveChangesAsync();
			//stopwatch.Stop();
			//return Ok(stopwatch.Elapsed);
			/* -- 1000 --
			 * "00:00:00.9822363"
			 * "00:00:00.1027283"
			 * -- 100_000 --
			 */
			//return Ok(count.ToString());
			return this.Ok();
			//stopwatch.Start();
			//for(int i = 0; i < 1000; i++) {
			//	await this.categoryWriteRepository.AddAsync(new() { Name = $"Deneme2 {i}" });
			//	await this.categoryWriteRepository.SaveChangesAsync();
			//}
			//stopwatch.Stop();
			//return Ok(stopwatch.Elapsed);
			//"00:00:03.5830169"
			//"00:00:01.9983436"

			//stopwatch.Start();
			//for(int i = 0; i < 100_000; i++) {
			//	await this.categoryWriteRepository.AddAsync(new() { Name = $"Deneme2 {i}" });
			//}
			//await this.categoryWriteRepository.SaveChangesAsync();
			//stopwatch.Stop();
			//return Ok(stopwatch.Elapsed);
			/* -- 1000 --
			 * "00:00:01.1471387"
			 * "00:00:00.1092607"
			 * -- 100_000 --
			 * "00:00:14.0023876"
			 * "00:00:13.0109912"
			 */


			//pagination.Page = 0;
			//pagination.Size = 2;
			//pagination.Count = this.categoryRepository.GetCount();
			//var categories = this.categoryRepository.GetList().Skip(pagination.Page * pagination.Size).Take(pagination.Size);
			//return Ok(new {
			//    categories,
			//    pagination
			//});
		}

		/// <summary>
		/// List Categories
		/// </summary>
		/// <remarks>bla bla bla Categories</remarks>
		/// <return>Categories List</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<IQueryable<ListCategoryDto>>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getDetailsAll")]
		public async Task<IActionResult> GetDetailsList([FromQuery] Pagination
			query) {
			return this.GetResponseDataResult(await this.Mediator.Send(new GetCategoriesDetailsQuery() {
				Page = query.Page,
				Size = query.Size
			}));
		}

		/// <summary>
		/// Get Categories Details By Id
		/// </summary>
		/// <remarks>bla bla bla Categories</remarks>
		/// <return>Categories List</return>
		/// <response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<IQueryable<GetCategoryDto>>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
		[HttpGet("getDetailsById")]
		public async Task<IActionResult> GetDetailsById([FromQuery] Guid id) {
			return this.GetResponseDataResult(await this.Mediator.Send(new GetCategoryDetailsQuery() { Id = id }));
		}

		/// <summary>
		/// Add Category
		/// </summary>
		/// <remarks>bla bla bla Category</remarks>
		/// <return>Category Add</return>
		/// <response code="201"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IResult))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreateCategoryCommand command) {
			return this.Create(await this.Mediator.Send(command));
		}

		/// <summary>
		/// Update Category
		/// </summary>
		/// <remarks>bla bla bla Category</remarks>
		/// <return>Category Update</return>
		/// <response code="204"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(IResult))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateCategoryCommand command) {
			return this.Update(await this.Mediator.Send(command));
		}

		/// <summary>
		/// Delete Category
		/// </summary>
		/// <remarks>bla bla bla Category</remarks>
		/// <return>Category Delete</return>
		/// <response code="204"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(IResult))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] DeleteCategoryCommand command) {
			return this.Remove(await this.Mediator.Send(command));
		}
	}
}
