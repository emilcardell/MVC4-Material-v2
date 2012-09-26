using System;

namespace WorkingExample.Models
{
    public class FavoritedProduct
    {
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public Product Product { get; set; }
    }
}