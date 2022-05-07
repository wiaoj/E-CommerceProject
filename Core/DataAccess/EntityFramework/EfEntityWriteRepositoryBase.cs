using Core.Entities.Abstract;
using Core.Utilities.Messages;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.Error;
using Core.Utilities.Results.Concrete.Success;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Core.DataAccess.EntityFramework {
	public class EfEntityWriteRepositoryBase<TypeEntity, TypeContext> : EfEntityRepositoryBase<TypeEntity, TypeContext>, IEntityWriteRepository<TypeEntity>
		where TypeEntity : class, IEntityBase, new()
		where TypeContext : DbContext {
		public EfEntityWriteRepositoryBase(TypeContext context) : base(context) { }

		public async Task<IResult> AddAsync(TypeEntity typeEntity) {
			EntityEntry<TypeEntity> entityEntry = await this.Table.AddAsync(typeEntity);
			//var entityEntry = context.Entry(typeEntity);
			//entityEntry.State = EntityState.Added;
			return entityEntry.State.Equals(EntityState.Added) ?
				new SuccessResult(WriteRepositoryBaseMessages.AddedEntity.Successful) :
				new ErrorResult(WriteRepositoryBaseMessages.AddedEntity.Unsuccessful);
		}

		public async Task<IResult> AddRangeAsync(IEnumerable<TypeEntity> datas) {
			await this.Table.AddRangeAsync(datas);
			return new SuccessResult();
		}

		public IResult Remove(TypeEntity typeEntity) {
			EntityEntry<TypeEntity> entityEntry = this.Table.Remove(typeEntity);
			//var entityEntry = context.Entry(typeEntity);
			//entityEntry.State = EntityState.Deleted;
			return entityEntry.State.Equals(EntityState.Deleted) ?
				new SuccessResult(WriteRepositoryBaseMessages.RemoveEntity.Successful) :
				new ErrorResult(WriteRepositoryBaseMessages.RemoveEntity.Unsuccessful);
		}

		public async Task<IResult> RemoveAsync(Guid id) {
			TypeEntity typeEntity = await this.Table.FindAsync(id);
			return this.Remove(typeEntity);
		}

		public IResult RemoveRange(IEnumerable<TypeEntity> datas) {
			this.Table.RemoveRange(datas);
			return new SuccessResult();
		}

		public async Task<IResult> SaveChangesAsync() {
			Boolean isSaved = (await this.context.SaveChangesAsync()).Equals(1);
			return isSaved ?
			 new InformationResult(WriteRepositoryBaseMessages.SaveDatabase.Information) :
			 new WarningResult(WriteRepositoryBaseMessages.SaveDatabase.Warning);

		}

		public IResult Update(TypeEntity typeEntity) {
			EntityEntry<TypeEntity> entityEntry = this.Table.Update(typeEntity);
			//var entityEntry = context.Entry(typeEntity);
			//entityEntry.State = EntityState.Modified;
			return entityEntry.State.Equals(EntityState.Modified) ?
				new SuccessResult() :
				new ErrorResult();
		}
	}
}