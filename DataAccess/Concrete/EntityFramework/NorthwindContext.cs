using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class NorthwindContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocalDB;Database=Northwind;Trusted_Connection=true");

            //Context hangi db'ye bağlanacağı bildirildi
            //Server belirtildi. ((Server=...... IP adressdir genellikle));
            //Hangi db olduğu belirtildi;
            //domain şifresine gerek olmadığı için Trusted_Connection=true verildi. Burada userName, password de girilebilir);
        }


        //Hangi nesnenin db'de hangi nesye karşılık geleceği bildirilir;
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

    }
}
