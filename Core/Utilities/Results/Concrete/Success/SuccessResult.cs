using Core.Utilities.Results.Abstract;

namespace Core.Utilities.Results.Concrete.Success {
	public record SuccessResult : ResultBase {
		public SuccessResult() : base(true, ResultStatus.Success) { }
		private protected SuccessResult(ResultStatus resultStatus) : base(true, resultStatus) { }
		public SuccessResult(String message) : base(true, ResultStatus.Success, message) { }

		//for ResultStatus.Information & Warning
		//TODO: Code refactor result information & warning
		private protected SuccessResult(ResultStatus status, String message) : base(true, status, message) { }

	}
	public record InformationResult : SuccessResult {
		public InformationResult() : base(ResultStatus.Information) { }
		public InformationResult(String message) : base(ResultStatus.Information, message) { }
	}
}