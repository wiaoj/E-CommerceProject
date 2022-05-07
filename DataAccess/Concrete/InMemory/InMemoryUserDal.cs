using Core.Entities.Concrete;

namespace DataAccess.Concrete.InMemory {
	public class InMemoryUserDal {
		public List<User> _user;
		public InMemoryUserDal() {
			this._user = new List<User> {
				new User {
					Id = new Guid(),
					FirstName = "Bertan",
					LastName = "Tokgöz",
					Email = "bertandeniz7@gmail.com",
					PhoneNumber = "05358788799"
				}
			};
		}
	}
}