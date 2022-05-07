using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract.Repositories.Users;

namespace DataAccess.Concrete.EntityFramework.Repositories.Users {
	public class UserWriteRepository : EfEntityWriteRepositoryBase<User, DataBaseContext>, IUserWriteRepository {
		public UserWriteRepository(DataBaseContext context) : base(context) { }
	}
}