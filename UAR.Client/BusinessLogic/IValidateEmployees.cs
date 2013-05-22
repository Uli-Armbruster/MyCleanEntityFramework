namespace UAR.Client.BusinessLogic
{
    interface IValidateEmployees
    {
        void EnsureValidHireDate(int employeeId);
    }
}