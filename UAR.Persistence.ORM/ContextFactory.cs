using System.Data.Entity;

using UAR.Persistence.Contracts;

namespace UAR.Persistence.ORM
{
    public class ContextFactory : IContextFactory
    {
        public DbContext Create()
        {
            return new AdventureWorksContext();
        }
    }
}