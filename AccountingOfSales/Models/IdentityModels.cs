using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingOfSales.Models
{
    class SalesDbContext : DbContext
    {
        public SalesDbContext(): base("DefaultConnection") { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<TypeProduct> TypeProducts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Admission> Admissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Return> Returns { get; set; }
        public DbSet<TypeReturn> TypeReturns { get; set; }
        public DbSet<OtherCosts> OtherCosts { get; set; }
        public DbSet<Salary> Salaries { get; set; }

    }
}
