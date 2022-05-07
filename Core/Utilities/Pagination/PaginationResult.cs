using Core.Entities.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using System.Collections;

namespace Core.Utilities.Pagination {
	public interface IPagination {
		public Int32 Page { get; set; }
		public Byte Size { get; set; }
	}
	public record Pagination : IPagination {
		public Int32 Page { get; set; }
		public Byte Size { get; set; }
	}
	public record PaginationResult<Type> : SuccessDataResult<Type>, IPagination {
		public PaginationResult(Type data) : this(data, 1, 50, 50) { }
		public PaginationResult(Type data, Int32 page, Byte pageSize, Int64 count) : base(data) {
			this.Page = page switch {
				<= 0 => 1,
				_ => page
			};
			this.Size = pageSize switch {
				<= 0 => 16,
				_ => pageSize
			};
			this.Count = count;
		}
		public Int32 Page { get; set; }
		public Byte Size { get; set; }
		public Int64 Count { get; set; }

		//public Boolean IsSucceed { get; }

		//public ResultStatus StatusCode { get; }

		//public String Status { get; }

		//public DateTime TimeUTC { get; }

		//public String Message { get; }

		//public Type? Data { get; }


		//public Pagination() : this(0, 5) { }
		//public Pagination(Int32 page, Byte size) {
		//    Page = page;
		//    Size = size;
		//}
	}
}