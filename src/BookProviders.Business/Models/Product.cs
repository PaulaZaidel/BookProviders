using System;

namespace BookProviders.Business.Models
{
    public class Product : Entity
    {
        public Guid CatererId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public DateTime Created { get; set; }
        public bool Active { get; set; }

        //EF Relations
        public Caterer Caterer { get; set; }
    }
}