using Entities.Models.Imitator;
using Microsoft.EntityFrameworkCore;

namespace Entities.Context
{
    public class ImitatorContext: DbContext
    {
        public ImitatorContext(DbContextOptions<ImitatorContext> options) : base(options)
        {
           Database.Migrate();
        }
        
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Position> Positions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .HasMany(c => c.Positions)
                .WithOne().HasForeignKey(p => p.DeviceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Owner>()
                .HasOne(c => c.Device)
                .WithOne().HasForeignKey<Device>(c => c.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
