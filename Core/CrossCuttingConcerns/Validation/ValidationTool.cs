using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Validation
{
    //CrossCuttingConcerns uygulamanın çalışma sırasında her katmanında çalışır
    public static class ValidationTool
    {
        //NOTE: Static class’ın methodları da static olmalı C#'da
        //IValidator, FluentValidation'ın sınıfı olan AbstractValidator<T>'dan gelir. Her türlü sınıf için oluşturulan
        //.. validasyonları çekebilmek adına AbstractValidator<T>'da bulunan Interface çekilir ki bütün...
        //FluentValidation sınıflarına ulaşabilmek adına (Örn: ProductValidator, AbstractValidator<T> implement edilmişti.)

        //Ayrıca, nesneyi FluentValidation içerisindeki Validate methodunun parametresi olarak..
        //kullanabilmek adına object olarak entity method parametresi yapılır.


        //validator : doğrulama kurallarının olduğu class
        //entity doğrulanacak nesne
        public static void Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            //kontrolünü yapar ve hata döner
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
