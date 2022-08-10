using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();
        IDataResult<List<Product>> GetAllByCategoryId(int categoryID);

        //UnitPrice decimal olduğu için parametre min ve max decimal atandı.
        IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max);

        IResult Add(Product product);
        IDataResult<Product> GetById(int productId);
        IDataResult<List<ProductDetailDto>> GetProductDetails();

    }
}
