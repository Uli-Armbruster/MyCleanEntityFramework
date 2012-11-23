using System;
using System.Data.Entity;
using System.Linq;

using UAR.Persistence.Contracts;

namespace UAR.Persistence.ORM
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly IContextFactory _contextFactory;
        DbContext _context;

        public UnitOfWork(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void Commit()
        {
            GetOrCreateContext();
            _context.SaveChanges();
        }

        void GetOrCreateContext()
        {
            if (_context == null)
                _context = _contextFactory.Create();

            if (_context == null)
                throw new ApplicationException("can't create context");
        }

        public void Add<T>(T entity) where T : class
        {
            GetOrCreateContext();
            _context.Set<T>().Add(entity);
        }

        public void Remove<T>(T entity) where T : class
        {
            GetOrCreateContext();
            _context.Set<T>().Remove(entity);
        }

        public IQueryable<T> Entities<T>() where T : class
        {
            GetOrCreateContext();
            return _context.Set<T>();
        }

        public TResult ExecuteQuery<TResult, T>(IQuery<TResult, T> query) where T : class
        {
            GetOrCreateContext();
            return query.Execute(_context.Set<T>());
        }

        public string GetConnectionString()
        {
            GetOrCreateContext();
            return _context.Database.Connection.ConnectionString;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }
    }
}