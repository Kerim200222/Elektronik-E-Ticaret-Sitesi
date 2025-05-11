using Microsoft.EntityFrameworkCore;
using EntityLayer22.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer2.Context
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet <Cart> Carts { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categorys { get; set; }

        public DbSet<Sales> Sales { get; set; }

        public DbSet<User> Users { get; set; }

    }
}
