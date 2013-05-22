using System;
using System.Collections.Generic;
using System.Linq;

using Machine.Specifications;

namespace UAR.Domain.AdventureWorks.Queries
{
    class GetAddressByCitySpecs
    {
        [Subject(typeof(GetAddressByCity))]
        public class When_addresses_contains_exactly_one_matching_entry
        {
            static GetAddressByCity Sut;
            static List<Address> Addresses;
            static Address Actual;
            static Address TestCity;

            Establish context = () =>
            {
                TestCity = new Address {City = "test city"};
                Addresses = new List<Address>
                {
                    new Address {City = "Karlsruhe"},
                    TestCity
                };
                Sut = new GetAddressByCity("test city");
            };

            Because of = () =>
            {
                Actual = Sut.Execute(Addresses.AsQueryable());
            };

            It should_return_exactly_this_address = () => Actual.ShouldEqual(TestCity);
        }

        [Subject(typeof(GetAddressByCity))]
        public class When_addresses_contains_more_than_one_matching_entry
        {
            static GetAddressByCity Sut;
            static List<Address> Addresses;
            static Address Actual;
            static Address TestCity;

            Establish context = () =>
            {
                TestCity = new Address {City = "test city", AddressID = 1};
                Addresses = new List<Address>
                {
                    TestCity,
                    new Address {City = "test city", AddressID = 2}
                };
                Sut = new GetAddressByCity("test city");
            };

            Because of = () =>
            {
                Actual = Sut.Execute(Addresses.AsQueryable());
            };

            It should_return_the_first_address = () => Actual.ShouldEqual(TestCity);
        }

        [Subject(typeof(GetAddressByCity))]
        public class When_addresses_contains_no_matching_entry
        {
            static GetAddressByCity Sut;
            static IList<Address> Addresses;
            static Exception Exception;

            Establish context = () =>
            {
                Addresses = new List<Address>
                {
                    new Address {City = "Karlsruhe"}
                };

                Sut = new GetAddressByCity("test city");
            };

            Because of = () =>
            {
                Exception = Catch.Exception(() => Sut.Execute(Addresses.AsQueryable()));
            };

            It should_throw_an_InvalidOperationException = () => Exception.ShouldBeOfType<InvalidOperationException>();
        }
    }
}