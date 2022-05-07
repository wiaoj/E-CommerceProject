using Business.Constants.Messages;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Securing;
using Core.Aspects.Autofac.Transaction;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.OperationClaims;
using MediatR;
using System.Reflection;

namespace Business.Handlers.OperationClaims.Commands {
	[TransactionScopeAspect]
	public class CreateOperationClaimCommand : IRequest<IResult> {
		public String ClaimName { get; set; }

		public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, IResult> {
			private readonly IOperationClaimWriteRepository operationClaimWriteRepository;

			public CreateOperationClaimCommandHandler(IOperationClaimWriteRepository operationClaimWriteRepository) {
				this.operationClaimWriteRepository = operationClaimWriteRepository;
			}

			//[SecuredOperation(Priority = 1)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken) {
				//if (IsClaimExists(request.ClaimName)) {
				//    return new ErrorResult(Messages.OperationClaimExists);
				//}
				//var operationClaim = new OperationClaim {
				//    Name = request.ClaimName,
				//    Alias = request.Alias,
				//    Description = request.Description
				//};
				foreach(var operationName in GetOperationNames()) {
					if(this.IsClaimExists(operationName).Equals(default)) {
						//return new ErrorResult(Messages.OperationClaimExists);
						await this.operationClaimWriteRepository.AddAsync(new OperationClaim {
							Name = operationName
						});
					}
				}
				await operationClaimWriteRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Success);
			}

			private Boolean IsClaimExists(String claimName) {
				//return this.operationClaimWriteRepository.Enumerable().Any(operationClaim => operationClaim.Name.Equals(claimName));
				return false;
			}

			private static IEnumerable<String> GetOperationNames() {
				var assemblies = Assembly.GetExecutingAssembly().GetTypes().Where(x =>
						// runtime generated anonmous type'larin assemblysi olmadigi icin null cek yap
						x.Namespace != null && x.Namespace.StartsWith("Business.Handlers") && (x.Name.EndsWith("Command") || x.Name.EndsWith("Query")));

				return (from assembly in assemblies
						from nestedType in assembly.GetNestedTypes()
						from method in nestedType.GetMethods()
						where method.CustomAttributes.Any(attribute => attribute.AttributeType.Equals(typeof(SecuredAspect)))
						select assembly.Name).ToList();
			}
		}
	}
}