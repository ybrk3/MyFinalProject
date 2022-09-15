using Core.Utilities.IOC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.Mirosoft
{
    //Microsot cache servisi kullanılacak
    //Adapter Pattern; Microsoft'da var olan hard code'ları projenin sistemine uyarlanılması
    public class MemoryCacheManager : ICacheManager
    {
        IMemoryCache _memoryCache; //IMemoryCache Microsoft'un servisi;
                                   //Core içerisinde olduğu için WebAPI-Business-DAL sıralamasına giremez
                                   //O yüzden constructor üzerinde değilde;
                                   //CoreModule üzerinden instance'lanır ve WebAPI içerisinde AddDependecyResolvers();
                                   //olarak çalıştırılır.

        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
            //GetService IOC'ye bakar ve instance'ı var mı diye kontrol eder.
            //Eğer varsa _memoryCache'e verir
            //GetService instance'ı check ederken CoreModule'a gider !
        }
        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));  
            //duration'da belirtilen dakika zaman aralığında ("TimeSpan")
        }

        public T Get<T>(string key)
        {
           return _memoryCache.Get<T>(key);
        }
        public object Get (string key)
        {
            return _memoryCache.Get(key);
            //Tip dönüşümü, boxing yapılması gerekir
        }
        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _);
            //TryGetValue bool ve value döndürür ve key ve out value parametrelerini ister
            //Eğer out ile verilen parametereyi döndürmek istenilmiyorsa "out _" verilir
        }

        public void Remove(string key)
        {
           _memoryCache.Remove(key);    
        }

        public void RemoveByPattern(string pattern)
        {
            var cacheEntriesCollectionDefinition = typeof(MemoryCache)
                .GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            //Bellekte "MemoryCache" türünde olan cache'leri EntriesCollection'da bulur
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;
            //definition'ı _memoryCache olanları bul
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
                //Her bir elemanı gez 
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();
            //keysToRemove: linq içerisindeki değere uyanları bulur ve liste olarak tutar
            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
                //keysToRemove olanları bellekten uçuruyor
            }

            //Kullanımı;
            //[CacheRemoveAspect("IProductService.Get")] Product Service'de GET içerenleri kaldır
        }
    }
}
