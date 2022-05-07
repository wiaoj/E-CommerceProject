using Core.Utilities.Configuration;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.Redis;

namespace Core.CrossCuttingConcerns.Caching.Redis {
	internal class RedisCacheManager : ICacheManager {
		private readonly RedisEndpoint redisEndpoint;
		private readonly ICoreConfiguration coreConfiguration = ServiceTool.ServiceProvider.GetService<ICoreConfiguration>();
		private const String Section = "RedisConfiguration";
		private const String Host = "Host";
		private const String Port = "Port";
		//TODO: Belki başka redis paketi kullanılabilir
		public RedisCacheManager() {
			this.redisEndpoint = new RedisEndpoint(
				this.coreConfiguration.GetSection(Section, Host),
				Int32.Parse(this.coreConfiguration.GetSection(Section, Port))
				);
		}

		private void RedisInvoker(Action<RedisClient> redisAction) {
			using(var client = new RedisClient(this.redisEndpoint)) {
				redisAction.Invoke(client);
			}
		}

		public void Add(String key, Object value) {
			this.RedisInvoker(x => x.Add(key, value));
		}

		public void Add(String key, Object value, Int32 duration) {
			this.RedisInvoker(x => x.Add(key, value, TimeSpan.FromMinutes(duration)));
		}

		public Type? Get<Type>(String key) {
			Type? result = default;
			this.RedisInvoker(x => { result = x.Get<Type>(key); });
			return result;
		}

		public Object? Get(String key) {
			Object? result = default;
			this.RedisInvoker(x => { result = x.Get<Object>(key); });
			return result;
		}

		public Boolean IsAdd(String key) {
			Boolean isAdded = default;
			this.RedisInvoker(x => isAdded = x.ContainsKey(key));
			return isAdded;
		}

		public void Remove(String key) {
			this.RedisInvoker(x => x.Remove(key));
		}

		public void RemoveByPattern(String pattern) {
			this.RedisInvoker(x => x.RemoveByPattern(pattern));
		}
		public void Clear() {
			this.RedisInvoker(x => x.FlushAll());
		}
	}
}