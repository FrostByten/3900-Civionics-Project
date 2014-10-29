using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace _3900_Civionics.Models
{
    /// <summary>
    /// Description:
    ///     Represents the Project table,
    ///     which holds information on civil structure projects
    /// 
    /// Data Members:
    ///     int ID
    ///     string Name
    ///     string Description
    ///     DateTime DateAdded
    ///     ICollection Sensors
    /// </summary>
    public class Project
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }

        public virtual ICollection<Sensor> Sensors { get; set; }
    }

    /// <summary>
    /// Description:
    ///     The database context for the Project class
    /// 
    /// Data Members:
    ///     DbSet Projects
    /// </summary>
    public class ProjectDBContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
    }

    /// <summary>
    /// Description:
    ///     Represents the ProjectAccess table,
    ///     which controls user permissions to projects
    /// 
    /// Data Members:
    ///     int ProjectAccessID
    ///     int ProjectID
    ///     string UserName
    /// </summary>
    public class ProjectAccess
    {
        [Key]
        public int ProjectAccessID { get; set; }

        public int ProjectID { get; set; }
        public string UserName { get; set; }
    }

    /// <summary>
    /// Description:
    ///     The database context for the ProjectAccess class
    /// 
    /// Data Members:
    ///     DbSet ProjectAccessList
    /// </summary>
    public class ProjectAccessDBContext : DbContext
    {
        public DbSet<ProjectAccess> ProjectAccessList{ get; set; }
    }
}