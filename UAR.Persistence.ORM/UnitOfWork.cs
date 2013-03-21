using System;
using System.Linq;

using UAR.Persistence.Contracts;

namespace UAR.Persistence.ORM
{
    class UnitOfWork : IUnitOfWork
    {
        readonly IContextFactory _contextFactory;
        IDbContext _wrappedContext;

        public UnitOfWork(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void Commit()
        {
            ValidateContext();
            _wrappedContext.OriginContext.SaveChanges();
        }

        public void Add<T>(T entity) where T : class
        {
            GetOrCreateContext<T>();
            _wrappedContext.Set<T>().Add(entity);
        }

        public void Remove<T>(T entity) where T : class
        {
            GetOrCreateContext<T>();
            _wrappedContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> Entities<T>() where T : class
        {
            GetOrCreateContext<T>();
            return _wrappedContext.Set<T>();
        }

        public TResult ExecuteQuery<TResult, T>(IQuery<TResult, T> query) where T : class
        {
            GetOrCreateContext<T>();
            return query.Execute(_wrappedContext.Set<T>());
        }

        public string GetConnectionString()
        {
            ValidateContext();
            return _wrappedContext.OriginContext.Database.Connection.ConnectionString;
        }

        public TResult ExecuteFunction<TResult>(IValueFunction<TResult> valueFunction)
        {
            GetOrCreateContext(valueFunction.GetType());
            return valueFunction.Execute(_wrappedContext);
        }

        public int ExecuteStoredProcedure(IStoredProcedure storedProcedure)
        {
            GetOrCreateContext(storedProcedure.GetType());
            return storedProcedure.Execute(_wrappedContext);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            if (_wrappedContext != null)
            {
                //Todo: release from container in order to memory consumption
                _wrappedContext.OriginContext.Dispose();
                _wrappedContext = null;
            }
        }

        void ValidateContext()
        {
            if (_wrappedContext == null)
            {
                throw new ApplicationException("context is null");
            }
        }

        void GetOrCreateContext<T>()
        {
            GetOrCreateContext(typeof(T));
        }

        void GetOrCreateContext(Type type)
        {
            if (_wrappedContext == null)
            {
                _wrappedContext = new WrappedContext(_contextFactory.Create(type));
            }

            if (_wrappedContext == null)
            {
                throw new ArgumentException();
            }
        }
    }
}