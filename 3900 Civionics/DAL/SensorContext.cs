using _3900_Civionics.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace _3900_Civionics.DAL
{
    /// <summary>
    /// Description:
    ///     Coordinates Entity Framework functionality for the sensor data model.
    ///     Each DbSet is a table and each entity in the set is a row in the table.
    /// 
    /// Data Members:
    ///     DbSet<Project> Projects
    ///     DbSet<Sensor> Sensors
    ///     DbSet<Reading> Readings
    /// </summary>
    public class SensorContext : DbContext
    {
        public SensorContext() : base("SensorContext")
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<Reading> Readings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}