namespace Core.Utilities.Results.Concrete.Error {
	public record ErrorDataResult<Type> : DataResultBase<Type> {
		public ErrorDataResult() : base(default, Abstract.ResultStatus.Error, false) { }
		public ErrorDataResult(String message) : base(default, false, Abstract.ResultStatus.Error, message) { }
		public ErrorDataResult(Type? data) : base(data, Abstract.ResultStatus.Error, false) { }
		//TODO: Code refactor result information & warning
		public ErrorDataResult(Type? data, Abstract.ResultStatus status) : base(data, status, false) { }
		public ErrorDataResult(Type? data, String message) : base(data, false, Abstract.ResultStatus.Error, message) { }
		//TODO: Code refactor result information & warning
		public ErrorDataResult(Type? data, Abstract.ResultStatus status, String message) : base(data, false, status, message) { }
	}
}