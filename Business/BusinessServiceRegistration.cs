using Core.DependencyResolvers;
using Core.Extensions.ServiceCollection;
using Core.Utilities.IoC;
using DataAccess;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Business {
	public static class BusinessServiceRegistration {
		public static void AddBusinessServices(this IServiceCollection services) {
			services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());
			services.AddMediatR(System.Reflection.Assembly.GetExecutingAssembly());

			services.AddDependencyResolvers(new ICoreModule[] {
				new CoreModule()
			});

			services.AddDataAccessServices();
		}
	}
}