using Core.Entities.Abstract;
using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataAccess.Concrete.EntityFramework {
	/// <summary>
	/// Because this context is followed by migration for more than one provider
	/// works on PostGreSql db by default. If you want to pass sql
	/// When adding AddDbContext, use MsDbContext derived from it.
	/// </summary>
	public class DataBaseContext : DbContext {
		public DataBaseContext(DbContextOptions options) : base(options) { }


		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(Configuration.ConnectionString);

		public async Task<Int32> SaveChangesAsync() {
			return await this.SaveChangesAsync(default);
		}

		public override Task<Int32> SaveChangesAsync(CancellationToken cancellationToken) {
			var datas = this.ChangeTracker.Entries<EntityBase>();

			/*TODO: update işlemi yapıldığı zaman createddate sıfırlanıyor
			 *      automapper ile işlem yapıldığı için işlem başarılı oluyor 
			 *      ama mapper içinde bulunmayan kısımlar da siliniyor
			*/
			foreach(var data in datas) {
				_ = data.State switch {
					EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
					EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
					_ => DateTime.UtcNow
				};
			}

			return base.SaveChangesAsync(cancellationToken);
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Customer> Customers { get; set; }

		public DbSet<OperationClaim> OperationClaims { get; set; }
		public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

		public DbSet<Group> Groups { get; set; }
		public DbSet<GroupOperationClaim> GroupClaims { get; set; }
		public DbSet<UserGroup> UserGroups { get; set; }


		public DbSet<Log> Logs { get; set; }


		public DbSet<Product> Products { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
		public DbSet<Category> Categories { get; set; }

	}
}