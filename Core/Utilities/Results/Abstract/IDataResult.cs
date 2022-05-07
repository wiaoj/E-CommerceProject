using Core.Entities.Abstract;

namespace Core.Utilities.Results.Abstract {
	public interface IDataResult<out Type> : IResult {
		Type? Data { get; }
	}
}