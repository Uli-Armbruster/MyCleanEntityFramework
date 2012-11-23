using System.Data.Entity;

using UAR.Persistence.Contracts;

namespace UAR.Persistence.ORM
{
    public class ContextFactory : IContextFactory
    {
        readonly IConfigureDatabase _databaseConfig;

        public ContextFactory(IConfigureDatabase databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public DbContext Create()
        {
            return new AdventureWorksDbContext(_databaseConfig);
        }
    }
}