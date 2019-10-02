
using System;

namespace BookProviders.Business.Models
{
    public class Adress : Entity
    {
        public Guid CatererId { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        //EF Relations
        public Caterer Caterer { get; set; }
    }
}
