using DataAccess.Abstract.Repositories.Products;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess {
	public static class ServiceRegistration {
		//TODO: business startup yapılıp içine eklenebilir
		//program.cs içinde kullanılmaktadır
		public static void AddDataAccessServices(this IServiceCollection services) {
			services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(Configuration.ConnectionString));

			//services.AddScoped<IWriteRepository, WriteRepository>();
			//services.AddScoped<IReadRepository, ReadRepository>();

			//services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
			//services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();

			//services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
			//services.AddScoped<IProductReadRepository, ProductReadRepository>();
		}
	}
}