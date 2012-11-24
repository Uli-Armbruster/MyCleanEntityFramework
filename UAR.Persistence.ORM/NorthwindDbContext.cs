﻿using System.Data.Entity;
using System.Reflection;

using UAR.Persistence.Contracts;

namespace UAR.Persistence.ORM
{
    public class NorthwindDbContext : DbContext
    {
        public NorthwindDbContext(IConfigureDatabase databaseConfig)
            : base(nameOrConnectionString: databaseConfig.EntityConnectionString(MethodBase.GetCurrentMethod().DeclaringType))
        {
            Configuration.LazyLoadingEnabled = true;
        }
    }
}