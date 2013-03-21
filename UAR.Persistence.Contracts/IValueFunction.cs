namespace UAR.Persistence.Contracts
{
    public interface IValueFunction<out TResult>
    {
        TResult Execute(IDbContext wrappedContext);
    }
}