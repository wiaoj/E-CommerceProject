using Business.Constants.Messages;
using Core.Aspects.Autofac.Securing;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Error;
using Core.Utilities.Results.Concrete.Success;
using DataAccess.Abstract.Repositories.Groups;
using MediatR;

namespace Business.Handlers.Groups.Queries {
	public class SearchGroupsByNameQuery : IRequest<IDataResult<IQueryable<Group>>> {
#pragma warning disable CS8618 // Non-nullable property 'GroupName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
		public String GroupName { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'GroupName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

		public class SearchGroupsByNameQueryHandler : IRequestHandler<SearchGroupsByNameQuery, IDataResult<IQueryable<Group>>> {
			private readonly IGroupReadRepository groupReadRepository;
			private readonly IBusinessRules businessRules;
			public SearchGroupsByNameQueryHandler(IGroupReadRepository groupReadRepository,
				IBusinessRules businessRules) {
				this.groupReadRepository = groupReadRepository;
				this.businessRules = businessRules;
			}

			[SecuredAspect(Priority = 1)]
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
			public async Task<IDataResult<IQueryable<Group>>> Handle(SearchGroupsByNameQuery request, CancellationToken cancellationToken) {
				var result = this.businessRules.Run(StringLengthMustBeGreaterThanThree(request.GroupName));

				if(result != null)
					return new ErrorDataResult<IQueryable<Group>>(result.Message);

				return new SuccessDataResult<IQueryable<Group>>(
					this.groupReadRepository.GetWhere(
						x => x.Name.ToLower().Contains(request.GroupName.ToLower())));
			}
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
			//TODO validationaspect
			private static IResult StringLengthMustBeGreaterThanThree(String searchString) {
				if(searchString.Length >= 3)
					return new SuccessResult();

				return new ErrorResult(Messages.StringLengthMustBeGreaterThanThree);
			}
		}
	}
}