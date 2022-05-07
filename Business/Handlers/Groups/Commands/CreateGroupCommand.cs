using Business.Constants.Messages;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Securing;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.Groups;
using MediatR;

namespace Business.Handlers.Groups.Commands {
	public class CreateGroupCommand : IRequest<IResult> {
		public String GroupName { get; set; }

		public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, IResult> {
			private readonly IGroupWriteRepository groupWriteRepository;


			public CreateGroupCommandHandler(IGroupWriteRepository groupWriteRepository) {
				this.groupWriteRepository = groupWriteRepository;
			}

			[SecuredAspect(Priority = 1)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(CreateGroupCommand request, CancellationToken cancellationToken) {

				var group = new Group {
					Name = request.GroupName
				};

				await this.groupWriteRepository.AddAsync(group);
				await this.groupWriteRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Added);
			}
		}
	}
}