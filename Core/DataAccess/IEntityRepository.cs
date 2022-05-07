using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess {
	public interface IEntityRepository<TypeEntity> where TypeEntity : class , IEntityBase, new() {
		DbSet<TypeEntity> Table { get; }
	}
}