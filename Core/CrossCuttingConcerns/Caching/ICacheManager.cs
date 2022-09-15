using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching
{

    //Farklı caching managerlar kullanılabilir. O yüzden interface olarak oluşturulur
    //Örn: Redis kullanılmak istenilebilir. Redis kodları (Redis folder içerisinde) ICacheManager implementasyonu ile yazılır,
    //...ve başka yerin değiştrilmesine gerek kalmaz
    public interface ICacheManager
    {
        T Get<T>(string key); //Generic method. Verilen key'e karşılık gelen data'ları çağırmak için.
                              //T liste, nesne vb. olarabilir
        object Get(string key); //Generic method kullanılmak istenmezse
                                //Bu da kullanılabilir ancak tip dönüşümü gerekir
        void Add(string key, object value, int duration); //Cache'e eklemek için
                                                          //key: o method'un unique adı Dosyalama isimleri kullanılacak
                                                          //value: gelecek (db'de saklanan) data
                                                          //duration: bunun cache'de ne kadar duracağı dakika veya saat olabilir
        bool IsAdd(string key); //Cache'de var mı kontrolü yapılır. Eğer varsa oradan eklenir yoksa Add methodu çalıştırılır
        void Remove(string key); //Cache'de update, add vb. methodlar çalıştırıldığında Cache'den silmek için
        void RemoveByPattern(string pattern); //Örn: içerisinde "Get" olanları Cache'den kaldırmak için kullanılabilir
    }
}
