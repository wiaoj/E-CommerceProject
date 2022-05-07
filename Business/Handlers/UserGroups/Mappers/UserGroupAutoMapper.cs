using AutoMapper;
using Business.Handlers.UserGroups.Commands;
using Core.Entities.Concrete;

namespace Business.Handlers.UserGroups.Mappers {
	internal class UserGroupAutoMapper : Profile {
		public UserGroupAutoMapper() {
			this.CreateMap<UserGroup, CreateUserGroupCommand>().ReverseMap();
			this.CreateMap<UserGroup, CreateUserGroupOperationClaimsCommand>()
				.ReverseMap();
			this.CreateMap<UserGroup, DeleteUserGroupCommand>().ReverseMap();
			this.CreateMap<UserGroup, UpdateUserGroupByGroupIdCommand>()
				//.ForMember(command => command.UserIds, option => option.MapFrom(x => x.UserId))
				.ReverseMap();
			this.CreateMap<UserGroup, UpdateUserGroupCommand>()
				.ReverseMap();
		}
	}
}