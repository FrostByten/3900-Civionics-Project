using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Civionics.Models
{
    // Enum defining the states a project can have
    public enum ProjectStatus
    {
        Safe, Warning, Alert
    }

    /// <summary>
    /// File: ProjectModels.cs
    /// 
    /// Designed by: Michael Chimick
    /// Created by: Sanders Lee
    /// Finished by: Michael Chimick
    /// Edited by: Lewis Scott
    ///            Michael Chimick
    /// 
    /// Class: Project
    /// 
    /// Data Members:
    ///     int ID                // database id for the project
    ///     string Name           // name of the structure
    ///     string Description    // description of the structure
    ///     ProjectStatus Status  // status object; deterimined by Sensor status'
    ///     DateTime DateAdded    // date project added to the system
    ///     
    ///     ICollection Sensors   // virtual collection of Sensor objects
    ///     
    /// Description:
    ///     Represents the Project table,
    ///     which holds information on civil structure projects
    /// </summary>
    public class Project
    {
        [Key, Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public ProjectStatus Status { get; set; }

        [Display(Name = "Date Added"), Required]
        public DateTime DateAdded { get; set; }

        public virtual ICollection<Sensor> Sensors { get; set; }
    }
}