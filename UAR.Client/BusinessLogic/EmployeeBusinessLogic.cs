using System;

using UAR.Domain.Northwind;
using UAR.Domain.Northwind.Queries;
using UAR.Persistence.Contracts;

namespace UAR.Client.BusinessLogic
{
    class EmployeeBusinessLogic : IValidateEmployees
    {
        readonly IUnitOfWork _uow;

        public EmployeeBusinessLogic(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void EnsureValidHireDate(int employeeId)
        {
            var employee = GetEmployee(employeeId);
            IfHireDateNotExistsSetToday(employee);
        }

        void IfHireDateNotExistsSetToday(Employee employee)
        {
            if (!employee.HireDate.HasValue)
            {
                employee.HireDate = DateTime.Now;
                _uow.Commit();
                Console.WriteLine("Updated hire date of {0} {1} to {2}", employee.FirstName, employee.LastName, employee.HireDate.Value);
            }
        }

        Employee GetEmployee(int employeeId)
        {
            var getEmployeeByIdQuery = new GetEmployeeById(employeeId);
            var employee = _uow.ExecuteQuery(getEmployeeByIdQuery);
            return employee;
        }
    }
}