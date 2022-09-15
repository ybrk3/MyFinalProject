using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Mirosoft;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.IOC
{
    public class CoreModule : ICoreModule
    {
        //Uygulama çalıştığında çalısacak
        //Farklı projelerde de kullanılabilecek injection'lar için instance;
        //Proje instance'ları için Business.DependencyResolvers Autofac kullanılır
        //seviceCollection üzerinden IServiceCollection'a erişilir
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();
            //.NET'e ait, otomatik injection yapar
            //Redis için gerekli değil !

            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //Yapılan isteğin oluşmasından response'a kadar geçen süreçin takibini yapar
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
            //Cache instance'ı için. "Çünkü Core katmanı üzerinde"
            //Eğer Redis eklenmek istenirse MemoryCacheManager yerine Redis yazılır

            //Eğer class'ın interface'i yoksa sadece class'da tanımlanabilir
            serviceCollection.AddSingleton<Stopwatch>();
        }
    }
}
