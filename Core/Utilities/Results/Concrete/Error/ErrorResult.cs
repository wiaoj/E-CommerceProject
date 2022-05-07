using Core.Utilities.Results.Abstract;

namespace Core.Utilities.Results.Concrete.Error {
	public record ErrorResult : ResultBase {
		public ErrorResult() : base(false, ResultStatus.Error) { }
		private protected ErrorResult(ResultStatus resultStatus) : base(false, resultStatus) { }
		public ErrorResult(String message) : base(false, ResultStatus.Error, message) { }

		//for ResultStatus.Error & Warning
		//TODO: Code refactor result information & warning
		private protected ErrorResult(ResultStatus resultStatus, String message) : base(false, resultStatus, message) { }
	}

	public record WarningResult : ErrorResult {
		public WarningResult() : base(ResultStatus.Warning) { }
		public WarningResult(String message) : base(ResultStatus.Warning, message) { }
	}
}