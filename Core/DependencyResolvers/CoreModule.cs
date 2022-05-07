using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Utilities.Business;
using Core.Utilities.Business.Concrete;
using Core.Utilities.Configuration;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.IoC;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Core.DependencyResolvers {
	public class CoreModule : ICoreModule {
		public void Load(IServiceCollection services) {

			//Func<IServiceProvider, ClaimsPrincipal> getPrincipal = (sp) =>
			//    sp.GetService<IHttpContextAccessor>()?.HttpContext?.User ??
			//    new ClaimsPrincipal(new ClaimsIdentity(Utilities.Messages.Messages.Unknown));

			//services.AddScoped<IPrincipal>(getPrincipal);


			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddMemoryCache();

			services.AddSingleton<Stopwatch>();
			services.AddSingleton<FileLogger>();
			//services.AddMemoryCache(); //using Microsoft.Extensions.Caching.Memory;
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			//services.AddSingleton<ICacheManager, MemoryCacheManager>(); //cache modülünü enjecte ediyoruz
			//serviceCollection.AddSingleton<ICacheManager, RedisCacheManager>(); //yapmak yeterli üstü

			services.AddSingleton<IFileHelper, ImageHelperManager>();
			services.AddScoped<IBusinessRules, BusinessRules>();

			services.AddTransient<ITokenHelper, JwtHelper>();
			//services.AddTransient<IElasticSearch, ElasticSearchManager>();

			//services.AddTransient<IMessageBrokerHelper, MqQueueHelper>();
			//services.AddTransient<IMessageConsumer, MqConsumerHelper>();
			services.AddSingleton<ICacheManager, MemoryCacheManager>();
			services.AddSingleton<ICoreConfiguration, Configuration>();
			services.AddSingleton<ConfigurationManager>();

			//services.AddAutoMapper(Assembly.GetExecutingAssembly());
			//services.AddMediatR(Assembly.GetExecutingAssembly());
		}
	}
}