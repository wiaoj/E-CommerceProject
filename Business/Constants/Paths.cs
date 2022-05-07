namespace Business.Constants {
	public class Paths {
		private const String ImagePath = @"wwwroot\Uploads\Images";
		private static String ProductImagesPath => @$"{ImagePath}\Products\";
		public static String ProductImagesPathWithProductId(Guid productId) => $@"{ProductImagesPath}{productId}\";
		public static String DefaultProductImagePath => @$"{ImagePath}\DefaultImage\defaultimage.jpg";
	}
}