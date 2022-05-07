using Business.Constants.Messages;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Securing;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract;
using DataAccess.Abstract.Repositories.Groups;
using MediatR;

namespace Business.Handlers.Groups.Commands {
	public class UpdateGroupCommand : IRequest<IResult> {
		public Guid Id { get; set; }
		public String GroupName { get; set; }

		public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, IResult> {
			private readonly IGroupWriteRepository groupWriteRepository;

			public UpdateGroupCommandHandler(IGroupWriteRepository groupWriteRepository) {
				this.groupWriteRepository = groupWriteRepository;
			}

			[SecuredAspect(Priority = 1)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(UpdateGroupCommand request, CancellationToken cancellationToken) {
				var groupToUpdate = new Group {
					Id = request.Id,
					Name = request.GroupName
				};

				this.groupWriteRepository.Update(groupToUpdate);
				this.groupWriteRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Updated);
			}
		}
	}
}