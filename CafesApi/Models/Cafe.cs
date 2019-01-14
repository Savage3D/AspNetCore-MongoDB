using System.Collections.Generic;

namespace CafesApi.Models
{
    public class Cafe
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
