using Core.Entities.Abstract;
using System.Linq.Expressions;

namespace Core.DataAccess {
	public interface IOldEntityRepository<Type> where Type : class, IEntityBase, new() { //Type => class - IEntity ve newlenebilir olmalı diyoruz
		Task AddAsync(Type entity);
		Task UpdateAsync(Type entity);
		Task DeleteAsync(Type entity);

		IEnumerable<Type> GetList(Expression<Func<Type, Boolean>>? expression = null);
		Task<IEnumerable<Type>> GetListAsync(Expression<Func<Type, Boolean>>? expression = null);

		Task<Type?> GetByIdAsync(Guid id);

		Type? Get(Expression<Func<Type, Boolean>> expression);
		Task<Type?> GetAsync(Expression<Func<Type, Boolean>> expression);

		Int32 SaveChanges();
		Task<Int32> SaveChangesAsync();

		IEnumerable<Type> Enumerable();

		Task<Int32> Execute(FormattableString interpolatedQueryString);
		TypeResult? InTransaction<TypeResult>(Func<TypeResult> action, Action? successAction = null, Action<Exception>? exceptionAction = null);

		Int32 GetCount(Expression<Func<Type, Boolean>>? expression = null);
		Task<Int32> GetCountAsync(Expression<Func<Type, Boolean>>? expression = null);
	}
}