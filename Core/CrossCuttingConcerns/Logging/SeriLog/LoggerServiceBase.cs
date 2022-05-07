using Serilog;

namespace Core.CrossCuttingConcerns.Logging.SeriLog {
	public abstract class LoggerServiceBase {
		protected ILogger Logger { get; set; }

		public void Verbose(String message) => this.Logger.Verbose(message);
		public void Fatal(String message) => this.Logger.Fatal(message);
		public void Info(String message) => this.Logger.Information(message);
		public void Warn(String message) => this.Logger.Warning(message);
		public void Debug(String message) => this.Logger.Debug(message);
		public void Error(String message) => this.Logger.Error(message);
	}
}