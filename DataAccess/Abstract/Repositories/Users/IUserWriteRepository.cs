using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract.Repositories.Users {
	public interface IUserWriteRepository : IEntityWriteRepository<User> { }
}