using AutoMapper;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Entities.Dtos;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract;
using DataAccess.Abstract.Repositories.Users;
using MediatR;

namespace Business.Handlers.Users.Queries {
	public class GetUsersQuery : IRequest<IDataResult<IEnumerable<UserDto>>> {
		public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IDataResult<IEnumerable<UserDto>>> {
			private readonly IUserReadRepository userReadRepository;
			private readonly IMapper mapper;

			public GetUsersQueryHandler(IUserReadRepository userReadRepository, IMapper mapper) {
				this.userReadRepository = userReadRepository;
				this.mapper = mapper;
			}

			//[SecuredOperation(Priority = 1)]
			[PerformanceAspect(5)]
			[CacheAspect(10)]
			public async Task<IDataResult<IEnumerable<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken) {
				var userList = this.userReadRepository.GetAll();
				var userDtoList = userList.Select(user => this.mapper.Map<UserDto>(user)).ToList();

				return new SuccessDataResult<IEnumerable<UserDto>>(userDtoList);
			}
		}
	}
}