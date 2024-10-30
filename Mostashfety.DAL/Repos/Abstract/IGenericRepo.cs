using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mostashfety.DAL.Repos.Abstract
{
    public interface IGenericRepo<T>
    {
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        IEnumerable<T> GetAll();
        T Get(int id);
        int? SaveChanges();
    }
}
