using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    //java => IProductDao
    //db'de yapılacak operasyonlar
    public interface IProductDal : IEntityRepository<Product>
    {
        //Interface operasyonları default public, kendisi internal
        List<Product> GetAll(Expression<Func<Product, bool>> filter = null);

        //Lambda ile tek bir elemanı getirmek için
        Product Get(Expression<Func<Product, bool>> filter);

        void Add(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
        //List<Product> GetAllByCategory(int categoryId);
        //User'ın istediği/filtrelediği category'e göre Product'ları listeler/gösterir

    }
}
