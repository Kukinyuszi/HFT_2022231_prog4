using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esport.Repository.Interfaces
{
    public interface IRepository<T>
        where T : class
    {
        T GetOne(int id);

        IQueryable<T> GetAll();

        void Insert(T item);

        void Remove(int id);

        void Update(T item);
    }
}
