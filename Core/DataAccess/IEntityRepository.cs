using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    //generic constraint
    //where T : class denildiğinde class referans tip olabilir
    //where T : class, IEntity IEntity olabilir veya IEntity implemente eden bir nesne olabilir
    //where T : class, IEntity,new(), new'lenebilir olmalı. Yani IEntity olamaz, abstract olamaz
    public interface IEntityRepository<T> where T : class, IEntity,new()
    {
        //Lambda ile listeleme Expression/Predicate üzerinden yapılır. (Filtreleme ile listeleme)
        List<T> GetAll(Expression<Func<T,bool>> filter=null);

        //Lambda ile tek bir elemanı getirmek için
        T Get(Expression<Func<T, bool>> filter);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
     
    }
}
