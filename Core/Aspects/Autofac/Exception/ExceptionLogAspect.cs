using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.SeriLog;
using Core.Utilities.Interceptors.Autofac;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Core.Aspects.Autofac.Exception {
	/// <summary>
	/// ExceptionLogAspect
	/// </summary>
	public class ExceptionLogAspect : MethodInterception {
		private readonly LoggerServiceBase _loggerServiceBase;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public ExceptionLogAspect(Type loggerService)
		{
			if(loggerService.BaseType != typeof(LoggerServiceBase)) {
				throw new ArgumentException(AspectMessages.WrongLoggerType);
			}
			this._loggerServiceBase = (LoggerServiceBase)ServiceTool.ServiceProvider.GetService(loggerService);
			this._httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
		}

		protected override void OnException(IInvocation invocation, System.Exception e) {
			var logDetailWithException = this.GetLogDetail(invocation);

			/*logDetailWithException.ExceptionMessage = e is System.Exception
                ? string.Join(Environment.NewLine, (e as AggregateException).InnerExceptions.Select(x => x.Message))
                : e.Message;*/
			this._loggerServiceBase.Error(JsonConvert.SerializeObject(logDetailWithException));
		}

		private LogDetailWithException GetLogDetail(IInvocation invocation) {
			var logParameters = invocation.Arguments.Select((t, i) => new LogParameter {
				Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
				Value = t,
				Type = t.GetType().Name
			})
				.ToList();
			var logDetailWithException = new LogDetailWithException {
				MethodName = invocation.Method.Name,
				Parameters = logParameters,
				User = (this._httpContextAccessor.HttpContext == null ||
						this._httpContextAccessor.HttpContext.User.Identity.Name == null)
					? "?"
					: this._httpContextAccessor.HttpContext.User.Identity.Name
			};
			return logDetailWithException;
		}
	}
}