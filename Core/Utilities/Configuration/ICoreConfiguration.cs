namespace Core.Utilities.Configuration {
	public interface ICoreConfiguration {
		public String GetConnectionString(String database);
		public String GetSection(String section, String name);
	}
}