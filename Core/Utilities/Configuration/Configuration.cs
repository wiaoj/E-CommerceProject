using Core.Utilities.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.Configuration {
	public class Configuration : ICoreConfiguration {
		private readonly ConfigurationManager configurationManager = ServiceTool.ServiceProvider?.GetService<ConfigurationManager>()!;
		public Configuration() : this("appsettings") { }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="jsonFileName">without .json</param>
		public Configuration(String jsonFileName) {
			this.configurationManager.SetBasePath(Directory.GetCurrentDirectory());
			this.configurationManager.AddJsonFile($"{jsonFileName}.json");
		}

		public String GetConnectionString(String database) {
			return this.configurationManager.GetConnectionString(database);
		}

		public String GetSection(String section, String name) => this.configurationManager.GetSection(section)[name];
	}
}