using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.SeriLog;
using Core.Utilities.Interceptors.Autofac;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Core.Aspects.Autofac.Logging {
	/// <summary>
	/// LogAspect [LogAspect(typeof(FileLogger))] FileLogger == LoggerType
	/// </summary>
	public class LogAspect : MethodInterception {
		private readonly LoggerServiceBase loggerServiceBase;
		private readonly IHttpContextAccessor httpContextAccessor;
		public LogAspect(Type loggerService) {
			if(loggerService.BaseType != typeof(LoggerServiceBase)) {
				throw new ArgumentException(AspectMessages.WrongLoggerType);
			}

			this.loggerServiceBase = ServiceTool.ServiceProvider?.GetService(loggerService) as LoggerServiceBase;
			this.httpContextAccessor = ServiceTool.ServiceProvider?.GetService<IHttpContextAccessor>();
		}

		protected override void OnBefore(IInvocation invocation) {
			this.loggerServiceBase?.Info(this.GetLogDetail(invocation));
		}

		private String GetLogDetail(IInvocation invocation) {
			List<LogParameter> logParameters = new();
			for(var i = 0; i < invocation.Arguments.Length; ++i) {
				logParameters.Add(new LogParameter {
					Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
					Value = invocation.Arguments[i],
					Type = invocation.Arguments[i].GetType().Name,
				});
			}

			var logDetail = new LogDetail {
				MethodName = invocation.Method.Name,
				Parameters = logParameters,
				User = (this.httpContextAccessor.HttpContext is null ||
						this.httpContextAccessor.HttpContext.User.Identity.Name is null) ?
						"?" : this.httpContextAccessor.HttpContext.User.Identity.Name
			};
			return JsonConvert.SerializeObject(logDetail);
		}
	}
}