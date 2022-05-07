using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Helpers.FileHelper {
	public class ImageHelperManager : IFileHelper {
		private readonly String[] permittedExtension = { ".png", ".jpg", ".jpeg" };
		private readonly Int64 MaximumFileSize = 8_388_608;
		public async Task<String> Upload(String root, IFormFile file) {
			if(this.MaximumFileSize >= file.Length && file.Length > default(Int32)) {

				if(Directory.Exists(root).Equals(default)) {
					Directory.CreateDirectory(root);
				}

				String extension = Path.GetExtension(file.FileName).ToLowerInvariant();

				if(this.permittedExtension.Contains(extension).Equals(default)) {
					throw new System.Exception("Unsupported file");
				}

				String filePath = $"{Guid.NewGuid()}{extension}";

				using(FileStream fileStream = File.Create($"{root}{filePath}")) {
					await file.CopyToAsync(fileStream);
					await fileStream.FlushAsync();
					return filePath;
				}
			}
			return null;
		}

		public async Task Delete(String filePath) {
			if(File.Exists(filePath)) {
				File.Delete(filePath);
			}
		}

		public async Task<String> Update(String filePath, String root, IFormFile file) {
			await this.Delete(filePath);
			return await this.Upload(root, file);
		}
	}
}