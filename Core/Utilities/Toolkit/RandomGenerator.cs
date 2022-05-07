namespace Core.Utilities.Toolkit {
	public static class RandomGenerator {
		public static String RandomPasswordGenerator() => RandomPasswordGenerator(16);
		public static String RandomPasswordGenerator(Byte length) {
			String upperCase = "ABCDEFGHJKLMNOPQRSTUVWXYZ",
				   lowerCase = "abcdefghijklmnopqrstuvwxyz",
				   numbers = "0123456789",
				   others = "!@#$%^&*?_-";
			String validChars = $"{upperCase}{lowerCase}{numbers}{others}";
			Random random = new();

			Char[] password = new Char[length];
			for(Byte i = default; i < length; i++) {
				password[i] = validChars[random.Next(default, validChars.Length)];
			}
			return new String(password);
		}

		private static Random Random(Int32 seed) => new(seed);
		private static Random Random() => Random(DateTime.Now.GetHashCode());

		public static Int32 RandomNumberGenerator() => RandomNumberGenerator(100_000, 999_999);
		public static Int32 RandomNumberGenerator(Int32 min, Int32 max) => Random().Next(min, max);
		public static Int64 RandomNumberGenerator(Int64 min, Int64 max) => Random().NextInt64(min, max);
		public static Single RandomFloatNumberGenerator() => Random().NextSingle();
		public static Double RandomDoubleNumberGenerator() => Random().NextDouble();

	}
}