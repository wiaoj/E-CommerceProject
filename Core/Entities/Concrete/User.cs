using Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Concrete {
	public class User : EntityBase {
		public String FirstName { get; set; }
		public String LastName { get; set; }
		public String Email { get; set; }
		public String PhoneNumber { get; set; }

		/// <summary>
		/// True = Male/False = Female
		/// </summary>
		public Boolean Gender { get; set; }
		public DateTime BirthDate { get; set; } = DateTime.Now;
		public String RefreshToken { get; set; }

		//public DateTime RecordDate { get; set; } = DateTime.Now;
		//public DateTime UpdateContactDate { get; set; } = DateTime.Now;
		public Boolean Status { get; set; } = true;

		public Byte[] PasswordHash { get; set; }
		public Byte[] PasswordSalt { get; set; }

		[NotMapped]
		public String AuthenticationProviderType { get; set; } = "Person";
	}
}