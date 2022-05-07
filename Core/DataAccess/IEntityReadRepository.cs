using Core.Entities.Abstract;
using System.Linq.Expressions;

namespace Core.DataAccess {
	public interface IEntityReadRepository<TypeEntity> : IEntityRepository<TypeEntity> where TypeEntity : class, IEntityBase, new() {
		IQueryable<TypeEntity> GetAll();
		IQueryable<TypeEntity> GetAll(Boolean tracking);
		IQueryable<TypeEntity> GetWhere(Expression<Func<TypeEntity, Boolean>> expression);
		IQueryable<TypeEntity> GetWhere(Expression<Func<TypeEntity, Boolean>> expression, Boolean tracking);
		Task<TypeEntity> GetSingleAsync(Expression<Func<TypeEntity, Boolean>> expression);
		Task<TypeEntity> GetSingleAsync(Expression<Func<TypeEntity, Boolean>> expression, Boolean tracking);
		Task<TypeEntity> GetByIdAsync(Guid id);
		Task<TypeEntity> GetByIdAsync(Guid id, Boolean tracking);
		Task<Int32> GetCountAsync();
		Task<Int32> GetCountAsync(Expression<Func<TypeEntity, Boolean>> expression);
	}
}