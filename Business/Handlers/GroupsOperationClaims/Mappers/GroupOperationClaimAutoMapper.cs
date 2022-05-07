using AutoMapper;
using Business.Handlers.GroupsOperationClaims.Commands;
using Core.Entities.Concrete;

namespace Business.Handlers.GroupsOperationClaims.Mappers {
	internal class GroupOperationClaimAutoMapper : Profile {
		public GroupOperationClaimAutoMapper() {
			this.CreateMap<GroupOperationClaim, CreateGroupOperationClaimCommand>().ReverseMap();
			this.CreateMap<GroupOperationClaim, UpdateGroupOperationClaimCommand>().ReverseMap();
			this.CreateMap<GroupOperationClaim, DeleteGroupOperationClaimCommand>().ReverseMap();
		}
	}
}
