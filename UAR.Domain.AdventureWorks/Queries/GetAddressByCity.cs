using System.Linq;

using UAR.Persistence.Contracts;

namespace UAR.Domain.AdventureWorks.Queries
{
    public class GetAddressByCity : IQuery<Address, Address>
    {
        readonly string _cityName;

        public GetAddressByCity(string cityName)
        {
            _cityName = cityName;
        }

        public Address Execute(IQueryable<Address> entities)
        {
            return entities.First(a => a.City == _cityName);
        }
    }
}
