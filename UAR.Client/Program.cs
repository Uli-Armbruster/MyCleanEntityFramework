using System;

using UAR.Domain.AdventureWorks.Queries;
using UAR.Persistence.Contracts;

namespace UAR.Client
{
    class Program
    {
        static void Main()
        {
            var container = new Bootstrapper()
                .RegisterComponents()
                .Container;

            using (var uow = container.Resolve<IUnitOfWork>())
            {
                var query = new GetAddressByCity("Bothell");
                var address = uow.ExecuteQuery(query);
                Console.WriteLine("PLZ von Bothell: {0}", address.PostalCode);
            }

            
            Console.ReadLine();
        }
    }
}
