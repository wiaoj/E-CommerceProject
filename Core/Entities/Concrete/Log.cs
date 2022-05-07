using Core.Entities.Abstract;

namespace Core.Entities.Concrete {
	public class Log : EntityBase {
		public String MessageTemplate { get; set; }
		public String Level { get; set; }
		public DateTime TimeStamp { get; set; }
		public String Exception { get; set; }
	}
}