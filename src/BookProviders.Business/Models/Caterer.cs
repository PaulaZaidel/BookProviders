using System.Collections.Generic;

namespace BookProviders.Business.Models
{
    public class Caterer : Entity
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public CatererType CatererType { get; set; }
        public Adress Adress { get; set; }
        public bool Active { get; set; }

        //EF Relations
        public IEnumerable<Product> Products { get; set; }
    }
}