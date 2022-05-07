using Core.Utilities.Results.Abstract;

namespace Core.Utilities.Results {
	public record ResultBase : IResult {
		public virtual Boolean IsSucceed { get; }
		public virtual ResultStatus StatusCode { get; }
		public virtual String Status { get; }
		public virtual String Message { get; }
		public virtual DateTime TimeUTC { get; }


		//ResultType IResult<ResultType>.Status { get; }


		//public ResultBase(Boolean success) {
		//    this.Success = success;
		//    //this.StatusCode = default;
		//    //this.Status = Enum.GetName(typeof(ResultStatus), default);
		//    this.Message = default!;
		//    this.TimeUTC = DateTime.UtcNow;


		public ResultBase(Boolean isSucceed, ResultStatus status)/* : this(success)*/ {
			this.IsSucceed = isSucceed;
			this.StatusCode = status;
			this.Status = Enum.GetName(typeof(ResultStatus), status)!;
			this.Message = default!;
			this.TimeUTC = DateTime.UtcNow;
		}

		//public ResultBase(Boolean success, String message) : this(success, ResultStatus.Success) {
		//    this.Message = message;
		//}

		public ResultBase(Boolean isSucceed, ResultStatus status, String message) : this(isSucceed, status) {
			this.Message = message;
		}
	}
}