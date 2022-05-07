using AutoMapper;
using Business.Constants.Messages;
using Core.Aspects.Autofac.Caching;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Error;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.Users;
using MediatR;

namespace Business.Handlers.Users.Commands {
	public class CreateUserCommand : IRequest<IResult> {
		public Guid Id { get; set; }
		public String FirstName { get; set; }
		public String LastName { get; set; }
		public String Email { get; set; }
		public String PhoneNumber { get; set; }
		public Boolean Gender { get; set; }
		public Boolean Status { get; set; }
		public DateTime BirthDate { get; set; }
		public DateTime RecordDate { get; set; }
		//public String Address { get; set; }
		public DateTime UpdateContactDate { get; set; }
		public String Password { get; set; }


		public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IResult> {
			private readonly IUserWriteRepository userWriteRepository;
			private readonly IUserReadRepository userReadRepository;
			private readonly IMapper mapper;

			public CreateUserCommandHandler(IUserWriteRepository userWriteRepository,
				IUserReadRepository userReadRepository,
				IMapper mapper) {
				this.userWriteRepository = userWriteRepository;
				this.userReadRepository = userReadRepository;
				this.mapper = mapper;
			}

			//[SecuredAspect(Priority = 1)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(CreateUserCommand request, CancellationToken cancellationToken) {
				var isThereAnyUser = this.userReadRepository.GetWhere(u => u.Email.Equals(request.Email) || u.PhoneNumber.Equals(request.PhoneNumber));

				if(isThereAnyUser != null) {
					return new ErrorResult(Messages.NameAlreadyExist);
				}

				//User user = new() {
				//	FirstName = request.FirstName,
				//	LastName = request.LastName,
				//	Email = request.Email,
				//	PhoneNumber = request.PhoneNumber,
				//	Gender = request.Gender,
				//	Status = true,
				//	//Address = request.Address,
				//	BirthDate = request.BirthDate
				//};
				User user = this.mapper.Map<User>(request);
				await this.userWriteRepository.AddAsync(user);
				await this.userWriteRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Added);
			}
		}
	}
}