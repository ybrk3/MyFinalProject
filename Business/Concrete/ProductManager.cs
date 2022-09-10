using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
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
        ICategoryService _categoryService;
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductCountOfCategory(product.CategoryId),
                CheckIfProductNameExists(product.ProductName), CheckIfCategoryLimitExceeded());

            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }


        public IDataResult<List<Product>> GetAll()
        {
          
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
            if (result == null)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.ProductNotFound);
            }

            return new SuccessDataResult<List<ProductDetailDto>>(result);
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult();
        }

        //private çünkü bu methodun bu class içerisinde kullanılması istenmektedir.
        private IResult CheckIfProductCountOfCategory(int categoryId)
        {
            //Select count(*) from products where categoryuId=1;
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new ErrorResult();
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }
        private IResult CheckIfCategoryLimitExceeded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceeded);
            }
            return new SuccessResult();
        }
    }
}
