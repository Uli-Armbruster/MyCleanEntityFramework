using System.Collections.Generic;
using System.Linq;

using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using UAR.Persistence.Contracts;

namespace UAR.Persistence.ORM
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Components().ToArray());

            if (!container.Kernel.GetFacilities().Any(x => x is TypedFactoryFacility))
            {
                container.AddFacility<TypedFactoryFacility>();
            }
        }

        private static IEnumerable<IRegistration> Components()
        {
            yield return Component
                .For<IContextFactory>()
                .ImplementedBy<ContextFactory>()
                .LifestyleSingleton();

            yield return Component
                .For<IUnitOfWork>()
                .ImplementedBy<UnitOfWork>()
                .LifestyleTransient();
            
            yield return Component
                .For<IConfigureDatabase>()
                .ImplementedBy<DatabaseConfiguration>()
                .LifestyleTransient();

            yield return Component
                .For<AdventureWorksDbContext>()
                .LifestyleTransient();

            yield return Component
                .For<NorthwindDbContext>()
                .LifestyleTransient();
        }
    }
}
