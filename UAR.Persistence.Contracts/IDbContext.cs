using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace UAR.Persistence.Contracts
{
    public interface IDbContext
    {
        DbContext OriginContext { get; }

        IDbSet<T> Set<T>() where T : class;

        IEnumerable<T> SqlQuery<T>(string query, params Object[] parameters);

        int ExecuteSqlCommand(string command, params Object[] parameters);
    }
}