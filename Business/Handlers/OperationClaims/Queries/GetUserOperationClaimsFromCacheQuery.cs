using Business.Constants.Messages;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Securing;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security;

namespace Business.Handlers.OperationClaims.Queries {
	public class GetUserOperationClaimsFromCacheQuery : IRequest<IDataResult<IEnumerable<String>>> {
		public class GetUserOperationClaimsFromCacheQueryHandler : IRequestHandler<GetUserOperationClaimsFromCacheQuery, IDataResult<IEnumerable<String>>> {
			private readonly ICacheManager cacheManager;
			private readonly IHttpContextAccessor contextAccessor;

			public GetUserOperationClaimsFromCacheQueryHandler(ICacheManager cacheManager,
				IHttpContextAccessor contextAccessor) {
				this.cacheManager = cacheManager;
				this.contextAccessor = contextAccessor;
			}

			[SecuredAspect(Priority = 1)]
			[PerformanceAspect(5)]
			[CacheAspect(10)]
			public async Task<IDataResult<IEnumerable<String>>> Handle(GetUserOperationClaimsFromCacheQuery request, CancellationToken cancellationToken) {
				var userId = this.contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;

				if(userId is null) {
					throw new SecurityException(Messages.AuthorizationsDenied);
				}

				var operationClaims = this.cacheManager.Get($"{ CacheKeys.UserIdForClaim}={userId }") as IEnumerable<String>;

				return new SuccessDataResult<IEnumerable<String>>(operationClaims);
			}
		}
	}
}