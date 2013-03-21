namespace UAR.Persistence.Contracts
{
    public interface IStoredProcedure
    {
        int Execute(IDbContext wrappedContext);
    }
}