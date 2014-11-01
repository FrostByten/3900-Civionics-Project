using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Civionics.Models
{
    public enum ProjectStatus
    {
        Safe, Warning, Alert
    }

    /// <summary>
    /// Description:
    ///     Represents the Project table,
    ///     which holds information on civil structure projects
    /// 
    /// Data Members:
    ///     int ID
    ///     string Name
    ///     string Description
    ///     ProjectStatus Status
    ///     DateTime DateAdded
    ///     ICollection Sensors
    /// </summary>
    public class Project
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public ProjectStatus Status { get; set; }

        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }

        public virtual ICollection<Sensor> Sensors { get; set; }
    }
}