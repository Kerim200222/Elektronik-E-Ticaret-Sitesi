using DataAccessLayer2.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer2.Abstract
{
    public class GenericRepository<T> : IRepository<T> where T : class, new()
    {
        private readonly DataContext db;
        private readonly DbSet<T> data;

        public GenericRepository(DataContext context)
        {
            db = context;
            data = db.Set<T>();
        }

        public void Delete(T p)
        {
            data.Remove(p);
            db.SaveChanges();
        }

        public T GetById(int id)
        {
            return data.Find(id);
        }

        public void Insert(T p)
        {
            data.Add(p);
            db.SaveChanges();
        }

        public List<T> List()
        {
            return data.ToList();
        }

        public void Update(T p)
        {
            db.Entry(p).State = EntityState.Modified;
            db.SaveChanges();
        }
    }   
}
