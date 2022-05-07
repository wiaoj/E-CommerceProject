using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors.Autofac;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security;

namespace Core.Aspects.Autofac.Securing {
	/// <summary>
	/// This Aspect control the user's roles in HttpContext by inject the IHttpContextAccessor.
	/// It is checked by writing as [SecuredOperation] on the handler.
	/// If a valid authorization cannot be found in aspect, it throws an exception.
	/// </summary>
	public class SecuredAspect : MethodInterception {
		private readonly IHttpContextAccessor? httpContextAccessor;
		private readonly ICacheManager? cacheManager;


		public SecuredAspect() {
			this.httpContextAccessor = ServiceTool.ServiceProvider?.GetService<IHttpContextAccessor>();
			this.cacheManager = ServiceTool.ServiceProvider?.GetService<ICacheManager>();
		}

		protected override void OnBefore(IInvocation invocation) {
			//String? userId = _httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
			String? userId = this.httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))?.Value;

			if(userId is null)
				throw new SecurityException(AspectMessages.AuthorizationsDenied);

			IEnumerable<String>? operationClaims = this.cacheManager?.Get<IEnumerable<String>>($"{CacheKeys.UserIdForClaim}={userId}");

			String? operationName = invocation.TargetType.ReflectedType?.Name;
			if(operationClaims is not null && operationClaims.Contains(operationName))
				return;

			throw new SecurityException(AspectMessages.AuthorizationsDenied);
		}
	}
}