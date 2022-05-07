using Business.Handlers.Authorizations.Queries;
using Business.Handlers.OperationClaims.Commands;
using Business.Handlers.OperationClaims.Queries;
using Business.Handlers.UserOperationClaims.Commands;
using Core.Aspects.Autofac.Securing;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.IoC;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Business.Helpers {
	[TransactionScopeAspect]
	public static class OperationClaimCreatorMiddleware {
		public static async Task UseDbOperationClaimCreator(this IApplicationBuilder app) {
			var mediator = ServiceTool.ServiceProvider?.GetService<IMediator>();
			foreach(var operationName in GetOperationNames()) {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
				await mediator.Send(new CreateOperationClaimCommand {
					ClaimName = operationName
					//Alias = String.Empty,
					//Description = String.Empty
				});
#pragma warning restore CS8602 // Dereference of a possibly null reference.
			}

#pragma warning disable CS8602 // Dereference of a possibly null reference.
			var operationClaims = (await mediator.Send(new GetOperationClaimsQuery())).Data;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
			var user = await mediator.Send(new UserRegisterQuery {
				FirstName = "System ",
				LastName = "Admin",
				Password = "String.7*"/*"K8wn51X9*_-"*/,
				Email = "admin@adminmail.com",
				PhoneNumber = "telefon"
			});
#pragma warning disable CS8601 // Possible null reference assignment.
			await mediator.Send(new CreateUserOperationClaimCommand {
				UserId = new Guid(),
				OperationClaims = operationClaims
			});
#pragma warning restore CS8601 // Possible null reference assignment.
		}

		public static IEnumerable<String> GetOperationNames() {
			var assemblies = Assembly.GetExecutingAssembly().GetTypes()
				.Where(x =>
					// runtime generated anonmous type'larin assemblysi olmadigi icin null cek yap
					x.Namespace is not null && x.Namespace.StartsWith("Business.Handlers") &&
					(x.Name.EndsWith("Command") || x.Name.EndsWith("Query")));

			return (from assembly in assemblies
					from nestedType in assembly.GetNestedTypes()
					from method in nestedType.GetMethods()
					where method.CustomAttributes.Any(u => u.AttributeType == typeof(SecuredAspect))
					select assembly.Name).ToList();
		}
	}
}