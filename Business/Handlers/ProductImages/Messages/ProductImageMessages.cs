namespace Business.Handlers.ProductImages.Messages {
	internal static class ProductImageMessages {
		private const String productImage = "The product image has been";
		public static String Added => $"{productImage} added.";
		public static String Updated => $"{productImage} updated.";
		public static String Deleted => $"{productImage} deleted.";
		public static String MaximumImageCount(Byte count) => $"The image adding limit has been reached ({count}), no more will be added.";
	}
}