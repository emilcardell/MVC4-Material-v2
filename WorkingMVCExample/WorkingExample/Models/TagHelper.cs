

using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using StructureMap;

namespace WorkingExample.Models
{
    public class TagHelper
    {
        public static List<string> Tags()
        {
            var session = ObjectFactory.GetInstance<IDocumentSession>();
            return session.Query<Product>().ToList().SelectMany(x => x.Tags).Distinct().ToList();
        }
    }
}