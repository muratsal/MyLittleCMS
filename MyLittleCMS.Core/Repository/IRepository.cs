using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MyLittleCMS.Core.Repository
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        IQueryable<T> Get();
        T First(Expression<Func<T, bool>> where);
        IQueryable<T> Where(Expression<Func<T, bool>> where);
        T Add(T entity);
        void Delete(T entity);
        void Delete(IQueryable<T> entities);
        void Delete(int id);
        T Update(T entity);
        //todo include işlemi için şimdilik böyle repository patterne uygulanacak
        DbSet<T> Table();
    }
}
