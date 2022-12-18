using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MonitorParkingSpotFunctionApp;
//[assembly: FunctionsStartup(typeof(Startup))]

namespace MonitorParkingSpotFunctionApp
{

    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<ParkingOccupancyDataContext>(options =>
                SqlServerDbContextOptionsExtensions.UseSqlServer(options, Environment.GetEnvironmentVariable("DBConnectionString")!));
        }
    }
    public class Function1
    {
        [FunctionName("ProcessParkingSpot")]
        public void ProcessParkingSpot([ServiceBusTrigger("locationqueue", Connection = "ServiceBusConnectionString")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");

            var sensorData = JsonSerializer.Deserialize<ParkingSesnorData>(myQueueItem);
            var contextOptions = new DbContextOptionsBuilder<ParkingOccupancyDataContext>()
                .UseSqlServer(Environment.GetEnvironmentVariable("DBConnectionString"))
                .Options;

            using var context = new ParkingOccupancyDataContext(contextOptions);
            {
                var parkingEntry = new ParkingSesnorData { SensorLocation = sensorData.SensorLocation, OccupancyState = sensorData.OccupancyState, EventTime = DateTime.Now };

                var entry = context.ParkingSensorData.Find(sensorData.SensorLocation);
                if (entry != null)
                {
                    entry.OccupancyState = sensorData.OccupancyState;
                    entry.EventTime = DateTime.Now;
                    context.Update(entry);
                }
                else
                context.Add<ParkingSesnorData>(parkingEntry);
                context.SaveChanges();
            }


        }
    }

    public class ParkingSesnorData
    {
        [Key]
        public String SensorLocation { get; set; }
        public String OccupancyState { get; set; }
        public DateTime EventTime { get; set; }



    }

    public class ParkingOccupancyDataContext : DbContext
    {
        public ParkingOccupancyDataContext(DbContextOptions<ParkingOccupancyDataContext> options) : base(options)
        {
        }
        public DbSet<ParkingSesnorData> ParkingSensorData { get; set; }

    }
}
