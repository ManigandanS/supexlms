using Lms.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Infrastructure.Repositories
{
    public class DbRepository<T> : IDbRepository<T> where T : class
    {
        private readonly ApplicationDbContext appDbContext;

        public DbRepository(ApplicationDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        

        #region IRepository<T> Members

        public virtual void Insert(T entity)
        {
            
            try
            {
                appDbContext.Entry(entity).State = EntityState.Added; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void Update(T entity)
        {

            try
            {
                appDbContext.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(T entity)
        {
            
            try
            {
                appDbContext.Entry(entity).State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Delete(string id)
        {
            try
            {
                appDbContext.Entry(GetById(id)).State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public virtual IQueryable<T> GetAll()
        {
            return appDbContext.Set<T>();
        }



        public virtual IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return appDbContext.Set<T>().Where(predicate);
        }


        public virtual IQueryable<T> GetAllAsNoTracking()
        {
            return appDbContext.Set<T>().AsNoTracking();
        }


        public virtual IQueryable<T> FindAsNoTracking(Expression<Func<T, bool>> predicate)
        {
            return appDbContext.Set<T>().AsNoTracking().Where(predicate).AsQueryable();
        }

        public virtual T GetById(string id)
        {
            T result = null;

            try
            {
                result = appDbContext.Set<T>().Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        #endregion
    }
}
