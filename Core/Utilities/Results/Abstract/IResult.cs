namespace Core.Utilities.Results.Abstract {
	public interface IResult {
		public Boolean IsSucceed { get; }
		public ResultStatus StatusCode { get; }
		public String Status { get; }
		public DateTime TimeUTC { get; }
		public String Message { get; }
	}
	public enum ResultStatus : Byte {
		Success = 0,
		Information = 1,
		Warning = 2,
		Error = 3
	}
}