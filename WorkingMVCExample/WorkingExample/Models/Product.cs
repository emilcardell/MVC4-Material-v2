using System.Collections.Generic;

namespace WorkingExample.Models
{
    public class Product
    {
        public Product()
        {
            Tags = new List<string>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string UrlToSmallImage { get; set; }
        public string UrlToLargeImage { get; set; }
        public int ImageId { get; set; }
        public List<string> Tags { get; set; }
    }
}