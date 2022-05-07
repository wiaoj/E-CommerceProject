using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework {
	public class EfEntityReadRepositoryBase<TypeEntity, TypeContext> : EfEntityRepositoryBase<TypeEntity, TypeContext>, IEntityReadRepository<TypeEntity>
		where TypeEntity : class, IEntityBase, new()
		where TypeContext : DbContext {
		public EfEntityReadRepositoryBase(TypeContext context) : base(context) { }

		public IQueryable<TypeEntity> GetAll() {
			return this.GetAll(true);
		}

		public IQueryable<TypeEntity> GetAll(Boolean tracking) {
			return tracking ?
				this.Table.AsQueryable() :
				this.Table.AsQueryable().AsNoTracking();
		}

		public async Task<TypeEntity> GetByIdAsync(Guid id) {
			return await this.GetByIdAsync(id, true);
		}

		public async Task<TypeEntity> GetByIdAsync(Guid id, Boolean tracking) {
			return await this.Table.FindAsync(id);
		}

		public async Task<Int32> GetCountAsync() {
			return await this.GetCountAsync(null);
		}
		public async Task<Int32> GetCountAsync(Expression<Func<TypeEntity, Boolean>> expression) {
			return expression is null ?
				await this.Table.CountAsync() :
				await this.Table.CountAsync(expression);
		}

		public async Task<TypeEntity> GetSingleAsync(Expression<Func<TypeEntity, Boolean>> expression) {
			return await this.GetSingleAsync(expression, true);
		}

		public async Task<TypeEntity> GetSingleAsync(Expression<Func<TypeEntity, Boolean>> expression, Boolean tracking) {
			return tracking ?
				await this.Table.AsQueryable().FirstOrDefaultAsync(expression) :
				await this.Table.AsQueryable().AsNoTracking().FirstOrDefaultAsync(expression);
		}

		public IQueryable<TypeEntity> GetWhere(Expression<Func<TypeEntity, Boolean>> expression) {
			return this.GetWhere(expression, true);
		}

		public IQueryable<TypeEntity> GetWhere(Expression<Func<TypeEntity, Boolean>> expression, Boolean tracking) {
			return tracking ?
				this.Table.Where(expression) :
				this.Table.Where(expression).AsNoTracking();
		}
	}
}