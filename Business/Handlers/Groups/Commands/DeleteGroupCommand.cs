using Business.Constants.Messages;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Securing;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.Groups;
using MediatR;

namespace Business.Handlers.Groups.Commands {
	public class DeleteGroupCommand : IRequest<IResult> {
		public Guid Id { get; set; }

		public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, IResult> {
			private readonly IGroupWriteRepository groupWriteRepository;

			public DeleteGroupCommandHandler(IGroupWriteRepository groupWriteRepository) {
				this.groupWriteRepository = groupWriteRepository;
			}

			[SecuredAspect(Priority = 1)]
			[CacheRemoveAspect()]
			public async Task<IResult> Handle(DeleteGroupCommand request, CancellationToken cancellationToken) {

				await this.groupWriteRepository.RemoveAsync(request.Id);
				await this.groupWriteRepository.SaveChangesAsync();

				return new SuccessResult(Messages.Deleted);
			}
		}
	}
}