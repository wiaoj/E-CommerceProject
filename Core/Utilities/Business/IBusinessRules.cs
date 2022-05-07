using Core.Utilities.Results.Abstract;

#nullable enable

namespace Core.Utilities.Business {
	public interface IBusinessRules {
		IResult? Run(params IResult[] logics);
	}
}