using Core.Utilities.Configuration;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess {
	internal static class Configuration {
		private const String ConnectionStringDataBaseName = "MsSQLConnectionString";
		private static readonly ICoreConfiguration configuration = ServiceTool.ServiceProvider?.GetService<ICoreConfiguration>()!;
		public static String ConnectionString {
			get {
				//ConfigurationManager configurationManager = new(); //.net 6
				////configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../WebAPI"));
				//configurationManager.SetBasePath(Directory.GetCurrentDirectory());
				//configurationManager.AddJsonFile("appsettings.json");
				//return configurationManager.GetConnectionString(ConnectionStringDataBaseName);
				//ConfigurationExtension configurationExtension = new();
				return configuration.GetConnectionString(ConnectionStringDataBaseName);
			}
		}
	}
}