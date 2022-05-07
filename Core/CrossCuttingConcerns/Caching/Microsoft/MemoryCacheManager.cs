using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace Core.CrossCuttingConcerns.Caching.Microsoft {
	/// <summary>
	/// Microsoft MemoryCacheManager
	/// </summary>
	public class MemoryCacheManager : ICacheManager {
		private readonly IMemoryCache memoryCache; //karşılığı Core.DependencyResolvers.ICoreModule
												   //ctor ile veremiyoruz şimdi webapi -> buiness -> dataacess diye ilerliyor zincir
		public MemoryCacheManager() : this(ServiceTool.ServiceProvider?.GetService<IMemoryCache>()!) { }

		private MemoryCacheManager(IMemoryCache memoryCache) {
			this.memoryCache = memoryCache;
		}

		public void Add(String key, Object value) => this.memoryCache.Set(key, value);
		public void Add(String key, Object value, Int32 duration) {
			//ne kadar süre verirsek o kadar süre cache kalacak
			this.memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));
		}

		public Type Get<Type>(String key) => this.memoryCache.Get<Type>(key);

		public Object Get(String key) => this.memoryCache.Get(key); //unboxing yapmak lazım object olduğu için

		public Boolean IsAdd(String key) => this.memoryCache.TryGetValue(key, out _); //eğer bir şey döndürmek istemiyorsak, böyle bir çözüm uygulayabiliriz -> c# karşılığı

		public void Remove(String key) => this.memoryCache.Remove(key);

		//reflection ile çalışma anında nesne oluşturma veya çalışma anında müdahale edebiliyoruz
		public void RemoveByPattern(String pattern) { //pattern oluşturma - çalışma anında bellekten silme işlemi
													  //bellekte memoryCache adında olan dataları entriescollection içine atılıyor (dökümantasyonda yazıyor)
			const String CollectionName = "EntriesCollection";
			var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty(CollectionName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
			dynamic cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(this.memoryCache); //definitionu _memoryCache olanı bul
			List<ICacheEntry> cacheCollectionValues = new();

			foreach(var cacheItem in cacheEntriesCollection) {
				ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
				cacheCollectionValues.Add(cacheItemValue);
			}

			Regex regex = new(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
			//cache elemanlarından bu kurala uyanları regex ile seçiyoruz
			List<Object> keysToRemove = cacheCollectionValues.Where(x => regex.IsMatch(x.Key.ToString())).Select(x => x.Key).ToList();

			foreach(Object key in keysToRemove) { //keyleri bulup 
				this.memoryCache.Remove(key); //bellekten siliyoruz
			} //patterna göre silme işlemi yapıyor
		} //memory cache dökümantasyonlarına gidip bakabiliriz
		  //bunları yapma sebebimiz ileride başka bir cache yönetimi yaparsak değiştiremeyiz
		  //bu yüzden kendi managerimiz içinde yapıyoruz
		  //microsoft kodunu kendimize uyguluyoruz buna Adapter Pattern deniliyor
	}
}