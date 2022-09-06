using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    //static olduğu için new'leme gerekmez.
    //Products bu projeye özgü olduğu için Core'a değil Business'a yazılır.
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string ProductsListed = "Ürünler listelendi";
        public static string ProductNotFound = "Ürün bulunamadı";
        public static string ProductCountOfCategoryError = "Bir kategoride en fazla 10 ürün olabilir.";
        public static string ProductNameAlreadyExists = "Aynı isimde ürün eklenemez";
        public static string CategoryLimitExceeded="Kategori limiti aşıldığı için ürün eklenemiyor";
    }
}
