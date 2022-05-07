using Core.Entities.Abstract;
using Core.Utilities.Results.Abstract;

namespace Core.DataAccess {
	public interface IEntityWriteRepository<TypeEntity> : IEntityRepository<TypeEntity> where TypeEntity : class, IEntityBase, new() {
		Task<IResult> AddAsync(TypeEntity typeEntity);
		Task<IResult> AddRangeAsync(IEnumerable<TypeEntity> datas);
		IResult Remove(TypeEntity typeEntity);
		IResult RemoveRange(IEnumerable<TypeEntity> datas);
		Task<IResult> RemoveAsync(Guid id);
		IResult Update(TypeEntity typeEntity);

		Task<IResult> SaveChangesAsync();
	}
}