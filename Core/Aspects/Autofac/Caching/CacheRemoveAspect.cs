using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors.Autofac;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching {
	/// <summary>
	/// Remove cache aspect [CacheRemoveAspect()]
	/// </summary>
	public class CacheRemoveAspect : MethodInterception { //data bozulunca yeni data eklenince falan çalışır
		private readonly String pattern = String.Empty;
		private readonly ICacheManager cacheManager;
		private const String CommandHandler = "CommandHandler";
		private const String Create = "Create";
		private const String Update = "Update";
		private const String Delete = "Delete";
		private const String Get = "Get";

		public CacheRemoveAspect() : this(String.Empty) { }
		public CacheRemoveAspect(String pattern) {
			this.pattern = pattern;
			this.cacheManager = ServiceTool.ServiceProvider?.GetService<ICacheManager>()!;
		}

		protected override void OnSuccess(IInvocation invocation) { //metod başarılı olursa cache siliniyor
																	//if (String.IsNullOrEmpty(this.pattern)) {
																	//    String targetTypeName = invocation.TargetType.Name;
																	//    targetTypeName = targetTypeName.Replace(CommandHandler, String.Empty);
																	//    targetTypeName = targetTypeName.Replace(Create, String.Empty);
																	//    targetTypeName = targetTypeName.Replace(Update, String.Empty);
																	//    targetTypeName = targetTypeName.Replace(Delete, String.Empty);
																	//    this.pattern = $"{Get}{targetTypeName}({BuildKey(invocation.Arguments)})";
																	//}
			this.cacheManager.RemoveByPattern(this.pattern);
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