using Core.Constants;
using Core.CrossCuttingConcerns.Logging.SeriLog.ConfigurationModels;
using Core.Utilities.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Core.CrossCuttingConcerns.Logging.SeriLog.Loggers {
	public class FileLogger : LoggerServiceBase {
		public FileLogger() {
			IConfiguration configuration = ServiceTool.ServiceProvider?.GetService<IConfiguration>();

			var logConfig = configuration.GetSection("SeriLogConfigurations:FileLogConfiguration")
								.Get<FileLogConfiguration>() ??
							throw new Exception(SeriLogMessages.NullOptionsMessage);

			var logFilePath = $"{Directory.GetCurrentDirectory()}{logConfig.FolderPath}.txt";

			this.Logger = new LoggerConfiguration().WriteTo.File(
					path: logFilePath,
					rollingInterval: RollingInterval.Day,
					retainedFileCountLimit: null,
					fileSizeLimitBytes: 5000000,
					outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
				.CreateLogger();
		}
	}
}