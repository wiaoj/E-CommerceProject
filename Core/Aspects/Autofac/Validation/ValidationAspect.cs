using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors.Autofac;
using Core.Utilities.Messages;
using FluentValidation;

namespace Core.Aspects.Autofac.Validation {
	public class ValidationAspect : MethodInterception /*Aspect --> metodun başında sonunda çalışacak şey demek*/{
		private readonly Type validatorType;

		public ValidationAspect(Type validatorType) {
			if(typeof(IValidator).IsAssignableFrom(validatorType).Equals(default))
				throw new ArgumentException(AspectMessages.WrongValidationType);
			this.validatorType = validatorType;
		}

		//[ValidationAspect(typeof(type))] motod başında yapılır
		protected override void OnBefore(IInvocation invocation) {
			IValidator? validator = Activator.CreateInstance(this.validatorType) as IValidator; //reflection productvalidatorun instancesini oluştur diyor
			Type? entityType = this.validatorType.BaseType?.GetGenericArguments()[0]; //productvalidatorun base tipini bulup generic argümanlarından ilkini bul
			IEnumerable<Object> entities = invocation.Arguments.Where(type => type.GetType().Equals(entityType)); //üsttekinin parametrelerini bul diyoruz
			foreach(Object entity in entities) {
				//bulunan parametleri validate et diyoruz
				ValidationTool.Validate(validator, entity);
			}
		}
	}
}