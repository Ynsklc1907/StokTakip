using StockMvc.Data.Entity;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace StockMvc.Data.Abstract
{
    public interface IRepository<T> : IDisposable where T : BaseEntity
    {
        void Add(T entity);
        void Remove(int id);
        void Update(T entity);
        T GetById(int id);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate = null, string includeProperties = "");
        IQueryable<T> CreateQuery(string includeProperties = "");
    }
}
