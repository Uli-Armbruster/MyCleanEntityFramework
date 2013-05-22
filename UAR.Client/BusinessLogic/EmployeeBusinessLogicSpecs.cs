using System.Linq;

using FakeItEasy;

using Machine.Specifications;

using UAR.Domain.Northwind;
using UAR.Domain.Northwind.Queries;
using UAR.Persistence.Contracts;

namespace UAR.Client.BusinessLogic
{
    class EmployeeBusinessLogicSpecs
    {
        [Subject(typeof(EmployeeBusinessLogic))]
        class When_hire_date_is_unknown
        {
            static IUnitOfWork Uow;
            static EmployeeBusinessLogic Sut;
            static Employee DummyEmployee;

            Establish context = () =>
            {
                DummyEmployee = new Employee {EmployeeID = 1, FirstName = "Uli", LastName = "Armbruster"};

                Uow = A.Fake<IUnitOfWork>();

                A
                    .CallTo(() => Uow.ExecuteQuery(
                        A<GetEmployeeById>.That.Matches(q => q.EmployeeId == DummyEmployee.EmployeeID)
                                      ))
                    .Returns(DummyEmployee);

                Sut = new EmployeeBusinessLogic(Uow);
            };

            Because of = () => Sut.EnsureValidHireDate(DummyEmployee.EmployeeID);

            It should_update_it = () => DummyEmployee.HireDate.ShouldNotBeNull();

            It should_save_the_changed_hire_date = () => A
                .CallTo(() => Uow.Commit())
                .MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}