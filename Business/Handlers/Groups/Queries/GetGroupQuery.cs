using Core.Aspects.Autofac.Securing;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.Groups;
using MediatR;

namespace Business.Handlers.Groups.Queries {
	public class GetGroupQuery : IRequest<IDataResult<Group>> {
		public Guid Id { get; set; }

		public class GetGroupQueryHandler : IRequestHandler<GetGroupQuery, IDataResult<Group>> {
			private readonly IGroupReadRepository groupReadRepository;

			public GetGroupQueryHandler(IGroupReadRepository groupReadRepository) {
				this.groupReadRepository = groupReadRepository;
			}

			[SecuredAspect(Priority = 1)]
			public async Task<IDataResult<Group>> Handle(GetGroupQuery request, CancellationToken cancellationToken) {
				var group = await this.groupReadRepository.GetSingleAsync(x => x.Id.Equals(request.Id));

				return new SuccessDataResult<Group>(group);
			}
		}
	}
}