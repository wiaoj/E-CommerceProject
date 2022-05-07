using AutoMapper;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Securing;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Entities.Dtos;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.Users;
using MediatR;

namespace Business.Handlers.Users.Queries {
	public class GetUserQuery : IRequest<IDataResult<UserDto>> {
		public Guid Id { get; set; }

		public class GetUserQueryHandler : IRequestHandler<GetUserQuery, IDataResult<UserDto>> {
			private readonly IUserReadRepository userReadRepository;
			private readonly IMapper mapper;

			public GetUserQueryHandler(IUserReadRepository userReadRepository, IMapper mapper) {
				this.userReadRepository = userReadRepository;
				this.mapper = mapper;
			}

			[SecuredAspect(Priority = 1)]
			public async Task<IDataResult<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken) {
				var user = await this.userReadRepository.GetSingleAsync(p => p.Id.Equals(request.Id));
				var userDto = this.mapper.Map<UserDto>(user);
				return new SuccessDataResult<UserDto>(userDto);
			}
		}
	}
}