using Castle.DynamicProxy;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using System.Reflection;

namespace Core.Utilities.Interceptors.Autofac {
	public class AspectInterceptorSelector : IInterceptorSelector {
		public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors) {
			var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
			var methodAttributes = type.GetMethod(method.Name)?.GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
			if(methodAttributes is not null) {
				classAttributes.AddRange(methodAttributes);
			}

			//classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger))); //default logs
			classAttributes.Add(new PerformanceAspect(5));
			classAttributes.Add(new LogAspect(typeof(FileLogger)));
			return classAttributes.OrderBy(x => x.Priority).ToArray();
		}
	}
}