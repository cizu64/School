using System;

namespace School.Domain.Entities.StudentAggregate
{
    public class Address 
    {
        public string City { get; private set; }
        public string Street { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }

        private Address() { }

        public Address(string city, string street, string state, string country)
        {
            City = city;
            Street = street;
            State = state;
            Country = country;
        }
    }
}
