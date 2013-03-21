using System;
using System.Linq;

using UAR.Persistence.Contracts;

namespace UAR.Domain.AdventureWorks.Functions
{
    public class GetProductListPrice : IValueFunction<decimal>
    {
        readonly int _productId;
        readonly DateTime _endDate;

        public GetProductListPrice(int productId, DateTime endDate)
        {
            _productId = productId;
            _endDate = endDate;
        }

        public decimal Execute(IDbContext wrappedContext)
        {
            var sqlQuery = string.Format("SELECT [dbo].[ufnGetProductListPrice] ({0}, CONVERT(DATETIME, '{1}-{2}-{3}', 102))", 
                _productId, _endDate.Year, _endDate.Month, _endDate.Day);
            return wrappedContext.SqlQuery<decimal>(sqlQuery).FirstOrDefault();
        }
    }
}