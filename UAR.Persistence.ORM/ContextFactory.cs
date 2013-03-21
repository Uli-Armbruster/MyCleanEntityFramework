using System;
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


        public DbContext Create(Type type)
        {
            var contextName = ContextNameByDomain(type);
            return _container.Resolve<DbContext>(contextName);
        }

        private string ContextNameByDomain(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (type.Namespace == null)
                throw new ArgumentException(string.Format("can't resolve namespace for type {0}", type.FullName));

            var path = type.Namespace.Split('.');

            if (path.Length < 3)
                throw new NotSupportedException(string.Format("can't resolve namespace for type {0}", type.FullName));

            var @namespace = string.Format("UAR.Persistence.ORM.{0}DbContext", path[2]);

            if (String.IsNullOrWhiteSpace(@namespace))
                throw new NotSupportedException(string.Format("can't resolve namespace for type {0}", type.FullName));

            return @namespace;
        }
    }
}