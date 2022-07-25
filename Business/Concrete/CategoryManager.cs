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
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public List<Category> GetAll()
        {
            //İş kodları (şartları)
            return _categoryDal.GetAll();
        }


        //Select * from Categories where CategoryId = 3
        public Category GetById(int id)
        {
            //İş kodları (şartları)
            return _categoryDal.Get(p => p.CategoryId == id);

        }
    }
}
