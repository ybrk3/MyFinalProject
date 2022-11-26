using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IResult Add(Category category)
        {
            _categoryDal.Add(category);
            return new SuccessResult(Messages.CategoryAdded);

        }

        public IDataResult<List<Category>> GetAll()
        {
            //İş kodları (şartları)
            
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll());
        }


        //Select * from Categories where CategoryId = 3
        public IDataResult<Category> GetById(int id)
        {
            //İş kodları (şartları)
            
            return new SuccessDataResult<Category>(_categoryDal.Get(p => p.CategoryId == id));

        }
    }
}
