using System.Data.Entity;

using Castle.Windsor;

using UAR.Persistence.Contracts;

namespace UAR.Persistence.ORM
{
    internal class ContextFactory : IContextFactory
    {
        readonly IWindsorContainer _container;

        public ContextFactory(IWindsorContainer container)
        {
            _container = container;
        }

        public DbContext Create<T>()
        {
            var contextName = ApplyContextNamingConvention<T>();

            return _container.Resolve<DbContext>(contextName);
        }

        static string ApplyContextNamingConvention<T>()
        {
            var modelName = typeof(T).Namespace.Replace("UAR.Domain.", "UAR.Persistence.ORM.");
            var contextName = string.Format("{0}DbContext", modelName);
            return contextName;
        }
    }
}