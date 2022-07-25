using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity,TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            //using'den gelen nesne kullanılıp, using bittikten sonra garbage collector'e nesne gelir ve bellekten atılır => Gereksiz yer tutmaz. using yazılmayabilir.
            //using == IDisposable Pattern Implementation of C#
            using (TContext context = new TContext())
            {
                //verilen product Entity'i db ile ilişkilendirir; Referans yakalar
                var addedEntity = context.Entry(entity);
                //verilen nesneyi ne yapacağı bildirilir
                addedEntity.State = Microsoft.EntityFrameworkCore.EntityState.Added;
                //Buraya koşullar girilik; SaveChanges() ile işleme onay verilir.
                context.SaveChanges(); ;
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                //Buraya koşullar girilik; SaveChanges() ile işleme onay verilir.
                context.SaveChanges();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //Buraya koşullar girilik; SaveChanges() ile işleme onay verilir.
                context.SaveChanges();
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                //Filtre girilmediyse select * from Products;
                //Filtre girildiyse hangi nesne/nesneyle tablo ilişkilendirilir ve .Where(filter) ile yakalanıp listeye çevirilir.
                return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();
            }
        }
        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                //context.Set<Product>() ile product db set'e bağlanır
                //filter'a uygun product'ı bulur ve Product nesnesiyle onu döndürür
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }
    }
}
