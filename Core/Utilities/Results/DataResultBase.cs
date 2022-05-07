using Core.Entities.Abstract;
using Core.Utilities.Results.Abstract;

namespace Core.Utilities.Results {
	public record DataResultBase<Type> : ResultBase, IDataResult<Type> {
		public override Boolean IsSucceed => base.IsSucceed;
		public override ResultStatus StatusCode => base.StatusCode;
		public override String Status => base.Status;
		public override String Message => base.Message;
		public override DateTime TimeUTC => base.TimeUTC;
		public virtual Type? Data { get; }

		public DataResultBase(Type? data, ResultStatus status, Boolean success) : base(success, status) {
			this.Data = data;
		}

		//public DataResultBase(Type? data, Boolean success, String message) : base(success, message) {
		//    this.Data = data;
		//}

		public DataResultBase(Type? data, Boolean success, ResultStatus status, String message) : base(success, status, message) {
			this.Data = data;
		}
	}
}