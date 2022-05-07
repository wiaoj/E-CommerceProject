using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors.Autofac;
using DataAccess.Abstract.Repositories.Categories;
using DataAccess.Abstract.Repositories.GroupClaims;
using DataAccess.Abstract.Repositories.Groups;
using DataAccess.Abstract.Repositories.Logs;
using DataAccess.Abstract.Repositories.OperationClaims;
using DataAccess.Abstract.Repositories.ProductImages;
using DataAccess.Abstract.Repositories.Products;
using DataAccess.Abstract.Repositories.UserGroups;
using DataAccess.Abstract.Repositories.UserOperationClaims;
using DataAccess.Abstract.Repositories.Users;
using DataAccess.Concrete.EntityFramework.Repositories.Categories;
using DataAccess.Concrete.EntityFramework.Repositories.GroupClaims;
using DataAccess.Concrete.EntityFramework.Repositories.Groups;
using DataAccess.Concrete.EntityFramework.Repositories.Logs;
using DataAccess.Concrete.EntityFramework.Repositories.OperationClaims;
using DataAccess.Concrete.EntityFramework.Repositories.ProductImages;
using DataAccess.Concrete.EntityFramework.Repositories.Products;
using DataAccess.Concrete.EntityFramework.Repositories.UserGroups;
using DataAccess.Concrete.EntityFramework.Repositories.UserOperationClaims;
using DataAccess.Concrete.EntityFramework.Repositories.Users;
using FluentValidation;
using MediatR;
//using MediatR;
using Module = Autofac.Module;
#nullable disable

namespace Business.DependencyResolvers.Autofac {
	public class AutofacBusinessModule : Module {
		protected override void Load(ContainerBuilder builder) {

			builder.RegisterType<CategoryWriteRepository>().As<ICategoryWriteRepository>();
			builder.RegisterType<CategoryReadRepository>().As<ICategoryReadRepository>().SingleInstance();

			builder.RegisterType<GroupClaimWriteRepository>().As<IGroupClaimWriteRepository>();
			builder.RegisterType<GroupClaimReadRepository>().As<IGroupClaimReadRepository>().SingleInstance();

			builder.RegisterType<GroupWriteRepository>().As<IGroupWriteRepository>();
			builder.RegisterType<GroupReadRepository>().As<IGroupReadRepository>().SingleInstance();

			builder.RegisterType<LogWriteRepository>().As<ILogWriteRepository>();
			builder.RegisterType<LogReadRepository>().As<ILogReadRepository>().SingleInstance();

			builder.RegisterType<OperationClaimWriteRepository>().As<IOperationClaimWriteRepository>();
			builder.RegisterType<OperationClaimReadRepository>().As<IOperationClaimReadRepository>().SingleInstance();

			builder.RegisterType<ProductImageWriteRepository>().As<IProductImageWriteRepository>();
			builder.RegisterType<ProductImageReadRepository>().As<IProductImageReadRepository>().SingleInstance();

			builder.RegisterType<ProductWriteRepository>().As<IProductWriteRepository>();
			builder.RegisterType<ProductReadRepository>().As<IProductReadRepository>().SingleInstance();

			builder.RegisterType<UserGroupWriteRepository>().As<IUserGroupWriteRepository>();
			builder.RegisterType<UserGroupReadRepository>().As<IUserGroupReadRepository>().SingleInstance();

			builder.RegisterType<UserOperationClaimWriteRepository>().As<IUserOperationClaimWriteRepository>();
			builder.RegisterType<UserOperationClaimReadRepository>().As<IUserOperationClaimReadRepository>().SingleInstance();

			builder.RegisterType<UserWriteRepository>().As<IUserWriteRepository>();
			builder.RegisterType<UserReadRepository>().As<IUserReadRepository>().SingleInstance();


			var assembly = System.Reflection.Assembly.GetExecutingAssembly();


			builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().AsClosedTypesOf(typeof(IRequestHandler<,>));
			builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().AsClosedTypesOf(typeof(IValidator<>));

			builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
				.EnableInterfaceInterceptors(new ProxyGenerationOptions() {
					Selector = new AspectInterceptorSelector()
				}).SingleInstance().InstancePerDependency();
		}
	}
}