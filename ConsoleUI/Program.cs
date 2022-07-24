// See https://aka.ms/new-console-template for more information

using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;

System.DateTime x = new System.DateTime();
ProductManager productManager = new ProductManager(new EfProductDal());
//() içerisinde kullanılacak entity'e uygun DataAccess class'ı newlenir/tanımlanır

foreach (var product in productManager.GetByUnitPrice(40,100))
{
    Console.WriteLine(product.ProductName);
}



