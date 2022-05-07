namespace Core.CrossCuttingConcerns.Caching {
	public interface ICacheManager { //başka cache implementleri eklemek için kullanıyoruz
									 //teknolojiden bağımsız interface
		Type Get<Type>(String key);
		Object Get(String key); //bunu yaparsak tip dönüşümü yapmamız lazım
		void Add(String key, Object value);
		void Add(String key, Object value, Int32 duration);
		Boolean IsAdd(String key); //cache de var mı diye kontrol
		void Remove(String key); //cache den silme işlemi
		void RemoveByPattern(String pattern); //regex ile metot ismi vererek silme işlemi
	}
}