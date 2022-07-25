using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICategoryDal : IEntityRepository<Category>
    {
        List<Category> GetAll(Expression<Func<Category, bool>> filter = null);

        //Lambda ile tek bir elemanı getirmek için
        Category Get(Expression<Func<Category, bool>> filter);

        void Add(Category entity);
        void Update(Category entity);
        void Delete(Category entity);
    }
}
