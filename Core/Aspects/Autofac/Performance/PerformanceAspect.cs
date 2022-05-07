using Castle.DynamicProxy;
using Core.Utilities.Interceptors.Autofac;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Core.Aspects.Autofac.Performance {
	//sistemde metotların çalışma sürelerini anlamamızı sağlıyor 
	//[PerformanceAspect(5)] ile 5 snden uzun sürerse çalışması uyarı gönderiyor
	public class PerformanceAspect : MethodInterception {
		private readonly Byte interval;
		private readonly Stopwatch? stopwatch;

		public PerformanceAspect() : this(8) { }
		public PerformanceAspect(Byte interval) {
			this.interval = interval;
			this.stopwatch = ServiceTool.ServiceProvider?.GetService<Stopwatch>();
		}


		protected override void OnBefore(IInvocation invocation) {
			this.stopwatch?.Start();
		}

		protected override void OnAfter(IInvocation invocation) {
			if(this.stopwatch?.Elapsed.TotalSeconds > this.interval)
				Debug.WriteLine($"Performance : { invocation.Method.DeclaringType?.FullName }.{ invocation.Method.Name } --> { this.stopwatch.Elapsed.TotalSeconds }");
			this.stopwatch?.Reset();
		}
	}
}