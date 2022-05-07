using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions.ServiceCollection {
	public static class ServiceCollectionExtensions {
		/* 
         * extensions yazabilmek için class static olmalı
         * apimizin servis bağımlılığı eklediğimiz yer
         * genişletmek istediğimizi this ile belirtiyoruz
         */
		public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection, params ICoreModule[] modules) {
			foreach(var module in modules) {
				module.Load(serviceCollection);
			}
			return ServiceTool.Create(serviceCollection);
			//services.AddDependencyResolvers(Configuration, new ICoreModule[] { coreModule });
		}
	}
}