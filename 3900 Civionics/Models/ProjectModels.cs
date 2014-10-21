using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace _3900_Civionics.Models
{
    public class Project
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }

    }

    public class ProjectDBContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
    }

    public class ProjectAccess
    {
        [Key]
        public int ProjectID { get; set; }

        public string UserName { get; set; }
    }

    public class ProjectAccessDBContext : DbContext
    {
        public DbSet<ProjectAccess> ProjectAccessList{ get; set; }
    }
}