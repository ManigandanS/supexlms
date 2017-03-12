using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Repositories
{
    public interface IDbRepository<T> where T : class
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(string id);
        void Delete(T entity);
        IQueryable<T> GetAll();
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAllAsNoTracking();
        IQueryable<T> FindAsNoTracking(Expression<Func<T, bool>> predicate);
        T GetById(string id);
    }
}
