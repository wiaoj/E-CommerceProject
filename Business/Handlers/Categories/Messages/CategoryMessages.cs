namespace Business.Handlers.Categories.Messages {
	internal class CategoryMessages {
		private const String category = "The category has been";
		public static String Added => $"{category} added.";
		public static String Updated => $"{category} updated.";
		public static String Deleted => $"{category} deleted.";
	}
}