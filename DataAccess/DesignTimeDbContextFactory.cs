using DataAccess.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess {
	internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DataBaseContext> {
		public DataBaseContext CreateDbContext(String[] args) {
			DbContextOptionsBuilder<DataBaseContext> dbContextOptionsBuilder = new();
			dbContextOptionsBuilder.UseSqlServer(Configuration.ConnectionString);
			return new(dbContextOptionsBuilder.Options);
		}
	}
}