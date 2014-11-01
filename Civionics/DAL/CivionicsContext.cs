using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using Civionics.Models;

namespace Civionics.DAL
{
    public class CivionicsContext : DbContext
    {
        public CivionicsContext() : base("CivionicsContext")
        { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectAccess> ProjectAccesses { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<Reading> Readings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}