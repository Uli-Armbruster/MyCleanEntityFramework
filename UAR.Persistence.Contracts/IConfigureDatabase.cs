namespace UAR.Persistence.Contracts
{
    public interface IConfigureDatabase
    {
        string EntityConnectionString(System.Type contextType);
    }
}