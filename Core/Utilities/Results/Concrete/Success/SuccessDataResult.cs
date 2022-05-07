using Core.Entities.Abstract;

namespace Core.Utilities.Results.Concrete.Success {
	public record SuccessDataResult<Type> : DataResultBase<Type> {
		public SuccessDataResult() : base(default, Abstract.ResultStatus.Success, true) { }
		public SuccessDataResult(String message) : base(default, true, Abstract.ResultStatus.Success, message) { }
		public SuccessDataResult(Type? data) : base(data, Abstract.ResultStatus.Success, true) { }
		//TODO: Code refactor result information & warning
		public SuccessDataResult(Type? data, Abstract.ResultStatus status) : base(data, status, true) { }
		public SuccessDataResult(Type? data, String message) : base(data, true, Abstract.ResultStatus.Success, message) { }
		//TODO: Code refactor result information & warning
		public SuccessDataResult(Type? data, Abstract.ResultStatus status, String message) : base(data, true, status, message) { }
	}
}