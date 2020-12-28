using Entities.Models;
using Microsoft.EntityFrameworkCore;


namespace Entities.Context
{
    public class SmartBoxContext: DbContext
    {
        public SmartBoxContext(DbContextOptions<SmartBoxContext> options) : base(options)
        {
            Database.Migrate();
        }


        public DbSet<SmartBox> SmartBoxes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<AlarmType> AlarmTypes { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<DriverHasBox> DriverHasBoxes { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<ModbusVariable> ModbusVariables { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderHasBox> OrderHasBoxes { get; set; }
        public DbSet<OrderStage> OrderStages { get; set; }
        public DbSet<OrderStageLog> OrderStageLogs { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<SensorData> SensorDatas { get; set; }
        public DbSet<SensorType> SensorTypes { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<UserHasAccess> UserHasAccesses { get; set; }
        public DbSet<UserHasOrder> UserHasOrders { get; set; }
        public DbSet<Variable> Variables { get; set; }
        public DbSet<VariableGroup> VariableGroups { get; set; }
        public DbSet<VariableNotify> VariableNotifies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SmartBox>()
                .HasMany(c => c.Locations)
                .WithOne().HasForeignKey(p => p.BoxId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SmartBox>()
                .HasMany(c => c.Alarms)
                .WithOne().HasForeignKey(p => p.BoxId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasMany(f => f.OrderHasBoxes)
                .WithOne().HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AlarmType>()
                .HasMany(c => c.Alarms)
                .WithOne().HasForeignKey(p => p.AlarmTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Driver>()
                .HasMany(c => c.DriverHasBoxes)
                .WithOne().HasForeignKey(p => p.DriverId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SmartBox>()
                .HasMany(c => c.DriverHasBoxes)
                .WithOne().HasForeignKey(p => p.BoxId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SmartBox>()
                .HasMany(c => c.Events)
                .WithOne().HasForeignKey(p => p.BoxId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Variable>()
                .HasMany(c => c.ModbusVariables)
                .WithOne().HasForeignKey(p => p.VariableId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Payment>()
                .HasMany(c => c.Orders)
                .WithOne().HasForeignKey(p => p.PaymentId)
                .OnDelete(DeleteBehavior.Cascade);//cascade

            modelBuilder.Entity<OrderStage>()
                .HasMany(c => c.Orders)
                .WithOne().HasForeignKey(p => p.OrderStageId)
                .OnDelete(DeleteBehavior.Cascade);//cascade

            modelBuilder.Entity<Order>()
                .HasMany(c => c.OrderStageLogs)
                .WithOne().HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderStage>()
                .HasMany(c => c.OrderStageLogs)
                .WithOne().HasForeignKey(p => p.OrderStageId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SensorType>()
                .HasMany(c => c.Sensors)
                .WithOne().HasForeignKey(p => p.SensorTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SmartBox>()
                .HasMany(c => c.Sensors)
                .WithOne().HasForeignKey(p => p.BoxId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Sensor>()
                .HasMany(c => c.SensorDatas)
                .WithOne().HasForeignKey(p => p.SensorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SmartBox>()
                .HasMany(c => c.DriverHasBoxes)
                .WithOne().HasForeignKey(p => p.BoxId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasMany(c => c.Tasks)
                .WithOne().HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Driver>()
                .HasMany(c => c.Tasks)
                .WithOne().HasForeignKey(p => p.DriverId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SmartBox>()
                .HasMany(c => c.UserHasAccesses)
                .WithOne().HasForeignKey(p => p.BoxId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SmartBox>()
                .HasMany(c => c.OrderHasBoxes)
                .WithOne().HasForeignKey(p => p.BoxId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasMany(c => c.UserHasOrders)
                .WithOne().HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.NoAction);



            modelBuilder.Entity<VariableGroup>()
                .HasMany(c => c.Variables)
                .WithOne().HasForeignKey(p => p.VariableGroupId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SmartBox>()
                .HasMany(c => c.Variables)
                .WithOne().HasForeignKey(p => p.BoxId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VariableGroup>()
                .HasMany(c => c.VariableGroupes)
                .WithOne().HasForeignKey(p => p.VariableGroupId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Variable>()
                .HasMany(c => c.VariableNotifies)
                .WithOne().HasForeignKey(p => p.VariableId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DriverHasBox>().HasKey(p => new { p.BoxId, p.DriverId });
            modelBuilder.Entity<UserHasOrder>().HasKey(p => new { p.OrderId, p.UserId });
            modelBuilder.Entity<OrderStageLog>().HasKey(p => new { p.OrderId, p.OrderStageId });
            modelBuilder.Entity<UserHasAccess>().HasKey(p => new { p.BoxId, p.UserId });
        }
    }
}
