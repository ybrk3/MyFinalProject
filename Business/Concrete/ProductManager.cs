using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IResult Add(Product product)
        {
            if (product.ProductName.Length < 2)
            {
                return new ErrorResult(Messages.ProductNameInvalid);
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IDataResult<List<Product>> GetAll()
        {
            //iş kodları, şartlar yazılır ona göre aşağıdaki method return edilir.
            //Yetkisi var mı? vb.
            if (DateTime.Now.Hour == 15)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);

        }

        public IDataResult<List<Product>> GetAllByCategoryId(int categoryID)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryID);

            if (result == null)
            {
                return new ErrorDataResult<List<Product>>(Messages.ProductNotFound);
            }
            return new SuccessDataResult<List<Product>>(result, Messages.ProductsListed);
        }

        public IDataResult<Product> GetById(int productId)
        {
            var result = _productDal.Get(P => P.ProductId == productId);
            if (result == null)
            {
                return new ErrorDataResult<Product>(Messages.ProductNotFound);
            }
            return new SuccessDataResult<Product>(result, Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            var result = _productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max);
            if (result == null)
            {
                return new ErrorDataResult<List<Product>>(Messages.ProductNotFound);
            }
            return new SuccessDataResult<List<Product>>(result, Messages.ProductsListed);
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {

            var result = _productDal.GetProductDetails();
            if (result==null)
            {
               return new ErrorDataResult<List<ProductDetailDto>>(Messages.ProductNotFound);
            }

            return new SuccessDataResult<List<ProductDetailDto>>(result);
        }
    }
}
