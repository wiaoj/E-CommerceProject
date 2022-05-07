using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Helpers.FileHelper {
	public interface IFileHelper {
		public Task<String> Upload(String root, IFormFile file);
		public Task Delete(String filePath);
		public Task<String> Update(String filePath, String root, IFormFile file);
	}
}