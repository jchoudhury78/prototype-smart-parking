using System.Collections.Generic;

namespace SmartParking.Domain
{
    public class Search
    {
        public string SearchResult { get; set; }
        public IEnumerable<ParkingSensorData> Parkings { get; set; }
    }
}
