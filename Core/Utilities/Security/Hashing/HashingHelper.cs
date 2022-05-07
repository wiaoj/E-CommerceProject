using System.Security.Cryptography;
using System.Text;

namespace Core.Utilities.Security.Hashing {
	public class HashingHelper {
		public static void CreatePasswordHash(String password, out Byte[] passwordHash, out Byte[] passwordSalt) {
			using HMACSHA512 hmac = new();
			passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
			passwordSalt = hmac.Key;
		}

		public static Boolean VerifyPasswordHash(String password, Byte[] passwordHash, Byte[] passwordSalt) {
			using HMACSHA512 hmac = new(passwordSalt);
			Byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
			for(Int32 i = 0; i < computedHash.Length; i++) {
				if(computedHash[i] != passwordHash[i]) {
					return false;
				}
			}
			return true;
		}
	}
}