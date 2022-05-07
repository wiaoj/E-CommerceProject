using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors.Autofac;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching {
	//reflectedType => Core.Aspect.Autofac.Caching.MethodInterception
	public class CacheAspect : MethodInterception {
		private readonly Int32 duration;
		private readonly ICacheManager cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();

		public CacheAspect() : this(60) { }
		public CacheAspect(Int32 duration) {
			this.duration = duration;
			//_cacheManager = ServiceTool.ServiceProvider?.GetService<ICacheManager>();
		}

		//TODO: code refactor
		public override void Intercept(IInvocation invocation) {
			//var methodName = String.Format($"{invocation.Arguments[0]}.{invocation.Method.Name}");
			//var arguments = invocation.Arguments;
			//var key = $"{methodName}({BuildKey(arguments)})";
			//if (this.cacheManager.IsAdd(key)) {
			//    invocation.ReturnValue = this.cacheManager.Get(key);
			//    return;
			//}

			//invocation.Proceed();
			//this.cacheManager.Add(key, invocation.ReturnValue, this.duration);
			var methodName = String.Format($"{invocation.Method.ReflectedType?.FullName}.{invocation.Method.Name}");
			var arguments = invocation.Arguments.ToList();
			var key = $"{methodName}({String.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
			if(this.cacheManager.IsAdd(key)) {
				invocation.ReturnValue = this.cacheManager.Get(key);
				return;
			}
			invocation.Proceed();
			this.cacheManager.Add(key, invocation.ReturnValue, this.duration);
		}

		private String BuildKey(Object[] args) {
			System.Text.StringBuilder sb = new();
			foreach(var arg in args) {
				var paramValues = arg.GetType().GetProperties()
					.Select(p => p.GetValue(arg)?.ToString() ?? String.Empty);
				sb.Append(String.Join('_', paramValues));
			}

			return sb.ToString();
		}
	}
}