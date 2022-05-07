using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework {
	public class EfEntityRepositoryBase<TypeEntity, TypeContext> : IEntityRepository<TypeEntity>
		where TypeEntity : class, IEntityBase, new()
		where TypeContext : DbContext {

		private protected readonly TypeContext context;
		public EfEntityRepositoryBase(TypeContext context) => this.context = context;
		public virtual DbSet<TypeEntity> Table => this.context.Set<TypeEntity>();
	}
}