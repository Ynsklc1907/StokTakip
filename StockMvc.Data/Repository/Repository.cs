using StockMvc.Data.Abstract;
using StockMvc.Data.Entity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace StockMvc.Data.Repository
{
    public sealed class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly Entity.DbEntities _context;
        private readonly DbSet<T> _dbSet;
        public Repository(Entity.DbEntities context)
        {
            _context = context ?? throw new ArgumentNullException("Context can not be null.");
            _dbSet = context.Set<T>();
        }

        private IQueryable<T> IncludeProperties(IQueryable<T> query, string includeProperties = "")
        {
            foreach (var includeProperty in includeProperties.Split
                          (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public IQueryable<T> CreateQuery(string includeProperties = "")
        {
            return IncludeProperties(_dbSet, includeProperties);
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Remove(int id)
        {
            var entity = GetById(id);
            entity.IsDeleted = true;
            Update(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate = null, string includeProperties = "")
        {
            var query = IncludeProperties(_dbSet, includeProperties);
            query = query.Where(k => !k.IsDeleted);

            if (predicate != null)
                query = query.Where(predicate);

            return query;
        }

        #region IDisposable implementation
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
