using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{

    //data'ya ulaşmak için kullanıcak yöntemlerden biri "InMemoryProduct". Bunlar Oracle, Sql için farklı yöntemler kullanılacaktır ve bunlar için ayrı .cs'ler kullanılır.
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        public InMemoryProductDal()
        {
            _products = new List<Product> {
            new Product { CategoryId = 1, ProductId = 1, ProductName = "Bardak", UnitPrice = 125, UnitsInStock = 15 },
            new Product { CategoryId = 1, ProductId = 2, ProductName = "Kamera", UnitPrice = 2500, UnitsInStock = 3 },
            new Product { CategoryId = 2, ProductId = 3, ProductName = "Telefon", UnitPrice = 9800, UnitsInStock = 2 },
            new Product { CategoryId = 2, ProductId = 4, ProductName = "Klavye", UnitPrice = 500, UnitsInStock = 65 },
            new Product { CategoryId = 2, ProductId = 5, ProductName = "Fare", UnitPrice = 185, UnitsInStock = 1 }
        };


            //db olmadığı için InMemoryProduct ile erişilecek örnek liste oluşturulur.
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            Product productToDelete = null;

            //_product listesindeki Product silineceği için Product değişkeni atanır.
            productToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId);

            //_products.SingleOrDefault yerine =>  _products.First / _products.FirstorDefault kullanılabilir
            _products.Remove(productToDelete);
        }

        public void Update(Product product)
        {
            Product productToUpdate = null;

            productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            //product'ın id'sine sahip olan Product _products listesinden bulunur.(aka.güncellenecek referans bulunur)
            //productToUpdate değişkenine verilir 

            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
            //productToUpdate'in property'lerine product'ın property'leri verilir ve productToUpdate istenilen güncellemelere sahip Product olur

            _products.Add(productToUpdate);
            //productToUpdate _products listesine eklenir
        }
        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}
