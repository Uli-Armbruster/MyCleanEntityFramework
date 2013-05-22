using System.Linq;

using UAR.Persistence.Contracts;

namespace UAR.Domain.Northwind.Queries
{
    public class GetEmployeeById : IQuery<Employee, Employee>
    {
        public int EmployeeId
        {
            get;
            private set;
        }

        public GetEmployeeById(int id)
        {
            EmployeeId = id;
        }

        public Employee Execute(IQueryable<Employee> entities)
        {
            return entities.Single(emp => emp.EmployeeID == EmployeeId);
        }
    }
}