using System;
using System.Collections.Generic;
using System.Text;

namespace Talabate.Core.Entity.Order_Agregate
{
    public class Address
    {
        public Address()
        {
            
        }

        public Address(string firestName, string lastName, string street, string city, string county)
        {
            FirestName = firestName;
            LastName = lastName;
            Street = street;
            City = city;
            County = county;
        }

        public string  FirestName { get; set; }
        public string  LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string County { get; set; }
    }
}
