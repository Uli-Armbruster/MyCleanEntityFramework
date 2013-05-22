using System;
using System.Linq;

using UAR.Client.BusinessLogic;
using UAR.Domain.AdventureWorks.Functions;
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

            int employeeId;
            using (var uow = container.Resolve<IUnitOfWork>())
            {
                var employee = uow.Entities<Employee>().First();
                Console.WriteLine("Northwind DB: Name des ersten Eintrags {0} {1}", employee.FirstName, employee.LastName);
                Console.WriteLine("HireDate: " + employee.HireDate);

                employee.HireDate = null;
                employeeId = employee.EmployeeID;
                uow.Commit();
            }

            var employeeBusinessLogic = container.Resolve<IValidateEmployees>();
            employeeBusinessLogic.EnsureValidHireDate(employeeId);
            container.Release(employeeBusinessLogic);


            using (var uow = container.Resolve<IUnitOfWork>())
            {
                var sqlFunction = new GetProductListPrice(707, new DateTime(2008, 1, 1));
                var productListPrice = uow.ExecuteFunction(sqlFunction);

                Console.WriteLine("Aufruf der SQL Funktion GetProductListPrice ergibt den Wert: {0}", productListPrice);
            }



            Console.ReadLine();
        }
    }
}
