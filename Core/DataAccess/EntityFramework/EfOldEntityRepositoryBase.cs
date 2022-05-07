using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework {
	public class EfOldEntityRepositoryBase<TypeEntity, TypeContext> :
		IOldEntityRepository<TypeEntity>
		where TypeEntity : class, IEntityBase, new()
		where TypeContext : DbContext/*, new() */{

		public EfOldEntityRepositoryBase(TypeContext context) {
			this.Context = context;
		}

		protected TypeContext Context { get; }
		//private static TypeContext CreateContext() => new();

		public async Task AddAsync(TypeEntity entity) {
			//using TypeContext context = CreateContext();
			var addedEntity = this.Context.Entry(entity);
			addedEntity.State = EntityState.Added; //ekle
			await this.Context.SaveChangesAsync(); //işlemleri kaydet
		}

		public async Task DeleteAsync(TypeEntity entity) {
			//using TypeContext context = CreateContext();
			var removedEntity = this.Context.Entry(entity);
			removedEntity.State = EntityState.Deleted;
			await this.Context.SaveChangesAsync();
		}

		public Task<Int32> Execute(FormattableString interpolatedQueryString) {
			throw new NotImplementedException();
		}

		public TypeEntity? Get(Expression<Func<TypeEntity, Boolean>> expression) {
			//using TypeContext context = CreateContext();
			return this.Context.Set<TypeEntity>().SingleOrDefault(expression);
		}

		public async Task<TypeEntity?> GetAsync(Expression<Func<TypeEntity, Boolean>> expression) {
			//using TypeContext context = CreateContext();
			return await this.Context.Set<TypeEntity>().AsQueryable().FirstOrDefaultAsync(expression);
		}

		public Int32 GetCount(Expression<Func<TypeEntity, Boolean>>? expression = null) {
			//using TypeContext context = CreateContext();
			return expression is null ?
				this.Context.Set<TypeEntity>().Count() :
				this.Context.Set<TypeEntity>().Count(expression);
		}

		public async Task<Int32> GetCountAsync(Expression<Func<TypeEntity, Boolean>>? expression = null) {
			//await using TypeContext context = CreateContext();
			return expression is null ?
				await this.Context.Set<TypeEntity>().CountAsync() :
				await this.Context.Set<TypeEntity>().CountAsync(expression);
		}

		public IEnumerable<TypeEntity> GetList(Expression<Func<TypeEntity, Boolean>>? expression = null) {
			//using TypeContext context = CreateContext();
			return expression is null ?
				this.Context.Set<TypeEntity>().AsNoTracking().ToList() :
				this.Context.Set<TypeEntity>().Where(expression).AsNoTracking().ToList();
		}

		public async Task<IEnumerable<TypeEntity>> GetListAsync(Expression<Func<TypeEntity, Boolean>>? expression = null) {
			//await using TypeContext context = CreateContext();
			return expression is null ?
				await this.Context.Set<TypeEntity>().ToListAsync() :
				await this.Context.Set<TypeEntity>().Where(expression).ToListAsync();
		}

		public TypeResult? InTransaction<TypeResult>(Func<TypeResult> action, Action? successAction = null, Action<Exception>? exceptionAction = null) {
			//using TypeContext context = CreateContext();
			var result = default(TypeResult);
			try {
				if(this.Context.Database.ProviderName.EndsWith("InMemory")) {
					result = action();
					this.SaveChanges();
				} else {
					using var transaction = this.Context.Database.BeginTransaction();
					try {
						result = action();
						this.SaveChanges();
						transaction.Commit();
					} catch(Exception) {
						transaction.Rollback();
						throw;
					}
				}
				successAction?.Invoke();
			} catch(Exception exception) {
				if(exceptionAction is null) {
					throw;
				}
				exceptionAction(exception);
			}

			return result;
		}

		public IEnumerable<TypeEntity> Enumerable() {
			//using TypeContext context = CreateContext();
			return this.Context.Set<TypeEntity>().ToList();
		}

		public Int32 SaveChanges() {
			throw new NotImplementedException();
		}

		public Task<Int32> SaveChangesAsync() {
			throw new NotImplementedException();
		}

		public async Task UpdateAsync(TypeEntity entity) {
			//using TypeContext context = CreateContext();
			var updatedEntity = this.Context.Entry(entity);
			updatedEntity.State = EntityState.Modified;
			await this.Context.SaveChangesAsync();
		}

		public async Task<TypeEntity?> GetByIdAsync(Guid id) {
			//using TypeContext context = CreateContext();
			return await this.Context.Set<TypeEntity>().FindAsync(id);
		}
	}
}