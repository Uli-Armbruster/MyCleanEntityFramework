using System.Collections.Generic;
using System.Linq;

using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using UAR.Client.BusinessLogic;

namespace UAR.Client
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
                .For<IValidateEmployees>()
                .ImplementedBy<EmployeeBusinessLogic>()
                .LifestyleTransient();
        }
    }
}