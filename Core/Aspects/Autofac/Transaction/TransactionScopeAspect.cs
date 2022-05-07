using Castle.DynamicProxy;
using Core.Utilities.Interceptors.Autofac;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction {
	public class TransactionScopeAspect : MethodInterception {
		// It is checked by writing as [TransactionScopeAspect] on the handler.

		//transaction yönetimi
		//2 hesap birbirine para gönderirken hata olursa işlemi geri almalı mesela
		//bu olayı bu şekilde interception olarak ekliyoruz metodun içine yazmak yerine
		public override void Intercept(IInvocation invocation) {
			using TransactionScope transactionScope = new();
			try {
				invocation.Proceed();
				transactionScope.Complete();
			} catch /*(Exception exception)*/ {
				transactionScope.Dispose();
				throw;
			}
		}
	}
}