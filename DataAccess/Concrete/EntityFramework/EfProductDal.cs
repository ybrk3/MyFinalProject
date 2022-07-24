using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : IProductDal
    {
        public void Add(Product product)
        {
            //using'den gelen nesne kullanılıp, using bittikten sonra garbage collector'e nesne gelir ve bellekten atılır => Gereksiz yer tutmaz. using yazılmayabilir.
            //using == IDisposable Pattern Implementation of C#
            using (NorthwindContext context = new NorthwindContext())
            {
                //verilen product Entity'i db ile ilişkilendirir; Referans yakalar
                var addedEntity = context.Entry(product);
                //verilen nesneyi ne yapacağı bildirilir
                addedEntity.State = Microsoft.EntityFrameworkCore.EntityState.Added;
                //Buraya koşullar girilik; SaveChanges() ile işleme onay verilir.
                context.SaveChanges(); ;
            }
        }

        public void Delete(Product product)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var deletedEntity = context.Entry(product);
                deletedEntity.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                //Buraya koşullar girilik; SaveChanges() ile işleme onay verilir.
                context.SaveChanges();
            }
        }

        public void Update(Product product)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var updatedEntity = context.Entry(product);
                updatedEntity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //Buraya koşullar girilik; SaveChanges() ile işleme onay verilir.
                context.SaveChanges();
            }
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                //Filtre girilmediyse select * from Products;
                //Filtre girildiyse hangi nesne/nesneyle tablo ilişkilendirilir ve .Where(filter) ile yakalanıp listeye çevirilir.
                return filter == null ? context.Set<Product>().ToList() : context.Set<Product>().Where(filter).ToList();
            }
        }
        public Product Get(Expression<Func<Product, bool>> filter)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                //context.Set<Product>() ile product db set'e bağlanır
                //filter'a uygun product'ı bulur ve Product nesnesiyle onu döndürür
                return context.Set<Product>().SingleOrDefault(filter);
            }
        }

    }
}
