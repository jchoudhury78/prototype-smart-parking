using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SmartParking.Domain
{
    public class ParkingDBConetxt: DbContext
    {
        

        public ParkingDBConetxt()
        {

        }
         

        
        public ParkingDBConetxt(DbContextOptions<ParkingDBConetxt> options) : base(options)
        {
           
        }

        public virtual DbSet<ParkingSensorData> ParkingSensorData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionService.Configuration.GetConnectionString("DefaultConnectionString"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<ParkingSensorData>(entity =>
            //{
            //    entity.ToTable("ParkingSensorData");
            //    entity.Property(e => e.SensorLocation).HasMaxLength(50).IsUnicode(false);
            //    entity.Property(e => e.OccupancyState).HasMaxLength(50).IsUnicode(false);
            //    entity.Property(e => e.EventTime);
            //});
            //OnModelCreatingPartial(modelBuilder);
        }
       // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
 