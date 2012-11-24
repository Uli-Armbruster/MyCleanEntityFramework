using System;
using System.Linq;

using UAR.Domain.AdventureWorks.Queries;
using UAR.Domain.Northwind;
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
                Console.WriteLine("AdventureWorks DB: PLZ von Bothell: {0}", address.PostalCode);
            }

            using (var uow = container.Resolve<IUnitOfWork>())
            {
                var employee = uow.Entities<Employee>().First();
                Console.WriteLine("Northwind DB: Name des ersten Eintrags {0} {1}", employee.FirstName, employee.LastName);
            }
            
            Console.ReadLine();
        }
    }
}
