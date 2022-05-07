using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors.Autofac {
	/// <summary>
	/// The Priority property can be used to determine the order in which Aspects will work on methods.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor {
		public Byte Priority { get; set; }
		//TODO MethodInterceptionBaseAttribute -> priotiry
		//public MethodInterceptionBaseAttribute(Byte priority) { this.Priority = priority; }
		//
		public virtual void Intercept(IInvocation invocation) { }
	}
}