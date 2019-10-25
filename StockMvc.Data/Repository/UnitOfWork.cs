using StockMvc.Data.Abstract;
using StockMvc.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace StockMvc.Data.Repository
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly Entity.DbEntities _context;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public UnitOfWork()
        {
            Database.SetInitializer<Entity.DbEntities>(null);
            _context = new Entity.DbEntities();
            _context.Configuration.LazyLoadingEnabled = false;
            _context.Configuration.ProxyCreationEnabled = false;
#if DEBUG
            _context.Database.Log = (s) => { System.Diagnostics.Debug.WriteLine(s); };
#endif
        }
        public IRepository<T> Repository<T>() where T : BaseEntity
        {
            if (_repositories.Keys.Contains(typeof(T)))
            {
                return _repositories[typeof(T)] as IRepository<T>;
            }
            else
            {
                IRepository<T> repo = new Repository<T>(_context);
                _repositories.Add(typeof(T), repo);
                return repo;
            }
        }
        public int SaveChanges()
        {

            var res = _context.SaveChanges();
            return res;


        }

        #region IDisposable implementation
        private bool disposed = false;
        private void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                _context.Dispose();
                foreach (var rep in _repositories)
                {
                    (rep.Value as IDisposable).Dispose();
                }
            }

            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
