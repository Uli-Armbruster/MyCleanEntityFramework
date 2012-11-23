using System.Linq;

namespace UAR.Persistence.Contracts
{
    public interface IQuery<out TResult, in TSource> where TSource : class
    {
        TResult Execute(IQueryable<TSource> entities);
    }
}