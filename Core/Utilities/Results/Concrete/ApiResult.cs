namespace Core.Utilities.Results.Concrete {
	public class ApiResult<Type> {
		public Boolean Success { get; set; }
		public String Message { get; set; }
		public String InternalMessage { get; set; }
		public Type Data { get; set; }
		public List<String> Errors { get; set; }
	}

	public class ApiReturn : ApiResult<Object> { }
}