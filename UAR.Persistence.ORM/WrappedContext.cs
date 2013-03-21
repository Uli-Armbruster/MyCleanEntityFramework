using System.Collections.Generic;
using System.Data.Entity;

using UAR.Persistence.Contracts;

namespace UAR.Persistence.ORM
{
    public class WrappedContext : IDbContext
    {
        readonly DbContext _context;

        public DbContext OriginContext
        {
            get
            {
                return _context;
            }
        }

        public WrappedContext(DbContext context)
        {
            _context = context;
        }

        public IDbSet<T> Set<T>() where T : class
        {
            return _context.Set<T>();
        }

        public IEnumerable<T> SqlQuery<T>(string query, params object[] parameters)
        {
            return _context.Database.SqlQuery<T>(query, parameters);
        }

        public int ExecuteSqlCommand(string command, params object[] parameters)
        {
            return _context.Database.ExecuteSqlCommand(command, parameters);
        }
    }
}