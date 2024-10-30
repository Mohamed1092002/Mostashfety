using Mostashfety.DAL.Context;
using Mostashfety.DAL.Repos.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mostashfety.DAL.Repos.Implementation
{
    public class GenericRepo<T>:IGenericRepo<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public GenericRepo(ApplicationDbContext context) 
        
        {
            _context = context;
        }

        public void Create(T item)
        {
            _context.Set<T>().Add(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Set<T>().Remove(Get(id));
        }

        public T Get(int id)
        {
            var res = _context.Set<T>().Find(id);
            return res;
        }

        public IEnumerable<T> GetAll()
        {
            var res= _context.Set<T>().ToList();
            return res;
        }

        public int? SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Update(T item)
        {
            _context.Set<T>().Update(item);
        }
    }
}
