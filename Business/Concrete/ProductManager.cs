using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {

        //Bir iş sınıfı başka bir iş sınıfını new'lemez o yüzden ctor üzerinden erişilir.
        //Böylelikle başka db'ye veya veri erişimine ihtiyaç duyulduğunda ctor parametresi üzerinde değiştirilebilir.
        //Bu sayede DataAccess Layer'da yer alan o db'ye özgü data erişim kodlarına erişilebilir.

        IProductDal _productDal;
        public ProductManager(IProductDal productDal )
        {
            _productDal = productDal;
        }
        public List<Product> GetAll()
        {
            //iş kodları, şartlar yazılır ona göre aşağıdaki method return edilir.
            //Yetkisi var mı? vb.
            return _productDal.GetAll();
        }

        public List<Product> GetAllByCategoryId(int categoryID)
        {
            return _productDal.GetAll(p=>p.CategoryId == categoryID);
        }

        public List<Product> GetByUnitPrice(decimal min, decimal max)
        {
            return _productDal.GetAll(p=> p.UnitPrice >= min && p.UnitPrice <= max);
        }
    }
}
