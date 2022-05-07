using AutoMapper;
using Business.Handlers.Users.Commands;
using Core.Entities.Concrete;

namespace Business.Handlers.Users.Mappers {
	internal class UserAutoMapper : Profile {
		public UserAutoMapper() {
			this.CreateMap<User, CreateUserCommand>().ReverseMap();
			this.CreateMap<User, UpdateUserCommand>().ReverseMap();
			//CreateMap<DeleteUserCommand, User>();
		}
	}
}