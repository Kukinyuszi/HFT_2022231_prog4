using Esport.Repository.Interfaces;
using Esport.Repository.Database;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esport.Repository.GenericRespository
{
    public abstract class MainRepository<T> : IRepository<T>
        where T : class
    {
        protected DataContext ctx;

        public MainRepository(DataContext ctx)
        {
            this.ctx = ctx;
        }

        public IQueryable<T> GetAll()
        {
            return this.ctx.Set<T>();
        }

        public abstract T GetOne(int id);

        public void Insert(T item)
        {
            ctx.Set<T>().Add(item);
            ctx.SaveChanges();
        }

        public void Remove(int id)
        {
            ctx.Set<T>().Remove(GetOne(id));
            ctx.SaveChanges();
        }

        public abstract void Update(T item);
    }
}
