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
    public class CategoryRepository: GenericRepository<Category>
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public List<Product> CategoryDetails(int id)
        {
            return _context.Products.Where(x => x.CategoryId == id).ToList();
        }

    }
}