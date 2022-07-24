using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICustomerDal : IEntityRepository<Customer>
    {

        List<Customer> GetAll(Expression<Func<Customer, bool>> filter = null);

        //Lambda ile tek bir elemanı getirmek için
        Customer Get(Expression<Func<Customer, bool>> filter);

        void Add(Customer entity);
        void Update(Customer entity);
        void Delete(Customer entity);
    }
}
