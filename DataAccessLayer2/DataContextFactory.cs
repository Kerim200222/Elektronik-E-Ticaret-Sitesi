using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DataAccessLayer2.Context;


namespace DataAccessLayer2
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer("Server=ABDULKARIM\\SQLEXPRESS;Database=E_TicaretDb;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=false;");

            return new DataContext(optionsBuilder.Options);
        }

    }
}
