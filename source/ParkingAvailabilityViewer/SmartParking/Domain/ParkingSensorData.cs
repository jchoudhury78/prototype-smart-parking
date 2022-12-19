using System;
using System.ComponentModel.DataAnnotations;

namespace SmartParking.Domain
{
    public class ParkingSensorData
    {
        [Key]
        public string SensorLocation { get; set; }
        public string OccupancyState { get; set; }
        public DateTime EventTime { get; set; }
    }
}
