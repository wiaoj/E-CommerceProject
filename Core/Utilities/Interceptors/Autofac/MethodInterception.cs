using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors.Autofac {
	public abstract class MethodInterception : MethodInterceptionBaseAttribute {
		protected virtual void OnBefore(IInvocation invocation) { }
		protected virtual void OnAfter(IInvocation invocation) { }
		protected virtual void OnException(IInvocation invocation, Exception exception) { }
		protected virtual void OnSuccess(IInvocation invocation) { }

		public override void Intercept(IInvocation invocation) {
			Boolean isSuccess = true;
			this.OnBefore(invocation);
			try {
				invocation.Proceed();
				Task? result = invocation.ReturnValue as Task;
				result?.Wait();
			} catch(Exception exception) {
				isSuccess = default;
				this.OnException(invocation, exception);
				throw;
			} finally {
				if(isSuccess)
					this.OnSuccess(invocation);
			}
			this.OnAfter(invocation);
		}
	}
	//TODO: Result Aspect
	public class ResultAspect : MethodInterception { }
}