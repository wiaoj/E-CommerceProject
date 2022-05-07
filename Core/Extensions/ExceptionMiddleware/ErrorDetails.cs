using FluentValidation.Results;
using Newtonsoft.Json;

namespace Core.Extensions.ExceptionMiddleware {
	public interface IErrorDetails {
		public Int32 StatusCode { get; set; }
		public DateTime TimeUTC { get; set; }
		public String Message { get; set; }
	}
	public interface IValidationErrorDetails : IErrorDetails {
		public IEnumerable<ValidationFailure> ValidationErrors { get; set; }
	}
	public interface ISecurityErrorDetails : IErrorDetails { }

	public class ErrorDetails : IErrorDetails {
		public ErrorDetails() : this(DateTime.UtcNow) { }
		public ErrorDetails(DateTime time) {
			this.TimeUTC = time;
		}

		public virtual Int32 StatusCode { get; set; }
		public virtual DateTime TimeUTC { get; set; }
		public virtual String Message { get; set; }
		public String ToJson() => JsonConvert.SerializeObject(this);
	}
	public class ValidationErrorDetails : ErrorDetails, IValidationErrorDetails {
		public ValidationErrorDetails() : base(DateTime.UtcNow) { }
		public override Int32 StatusCode { get; set; }
		public override DateTime TimeUTC { get; set; }
		public override String Message { get; set; }
		public IEnumerable<ValidationFailure> ValidationErrors { get; set; }
	}
	public class SecurityErrorDetails : ErrorDetails, ISecurityErrorDetails {
		public SecurityErrorDetails() : base(DateTime.UtcNow) { }
	}
}