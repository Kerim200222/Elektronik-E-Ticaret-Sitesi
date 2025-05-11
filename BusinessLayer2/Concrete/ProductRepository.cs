using BusinessLayer2.Abstract;
using DataAccessLayer2.Context;
using EntityLayer22.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer2.Concrete
{
    public class ProductRepository : GenericRepository<Product>
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public List<Product> GetPopularProduct()
        {
            return _context.Products.Where(x => x.Popular == true).Take(3).ToList();
        }

    }
}
