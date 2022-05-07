namespace Business.Handlers.Products.Messages {
	internal static class ProductMessages {
		private const String product = "The product has been";
		public static String Added => $"{product} added.";
		public static String Updated => $"{product} updated.";
		public static String Deleted => $"{product} deleted.";
	}
}