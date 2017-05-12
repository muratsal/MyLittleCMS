using MyLittleCMS.Core.Repository;
using MyLittleCMS.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleCMS.Data.Repositories
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        protected DbSet<T> _table;
        protected EFContext _context;

        public EFRepository(IEFContext dbContext)
        {
            _context = (EFContext)dbContext;
            _table = _context.Set<T>();
        }

        public T Get(int id)
        {
            return _table.Find(id);
        }

        public IEnumerable<T> Get()
        {
            return _table.AsEnumerable();
        }

        public T Add(T entity)
        {
            _table.Add(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public T Update(T entity)
        {
            var entityEntry = _context.Entry(entity);
            entityEntry.State = EntityState.Modified;
            return entity;
        }

        public T First(Expression<Func<T, bool>> where)
        {
            return _table.FirstOrDefault(where);
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> where)
        {
            return _table.Where<T>(where);
        }

        public void Delete(int id)
        {
            var entity = Get(id);
            Delete(entity);
        }

        public void Delete(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                _table.Remove(entity);
            }
        }
    }
}
