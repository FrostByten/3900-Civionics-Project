using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Civionics.Models
{
    /// <summary>
    /// File: ProjectAccessModels.cs
    /// 
    /// Designed by: Michael Chimick
    /// Created by: Sanders Lee
    /// Finished by: Michael Chimick
    /// Edited by: Lewis Scott
    ///            Michael Chimick
    /// 
    /// Class: ProjectAccess
    /// 
    /// Data Members:
    ///     int ProjectAccessID  // database id for the project access
    ///     int ProjectID        // id tying the access to a Project
    ///     string UserName      // id tying the access to a Sensor
    ///     
    /// Description:
    ///     Represents the ProjectAccess table,
    ///     which controls user permissions to projects
    /// </summary>
    public class ProjectAccess
    {
        [Key, Display(Name = "ID"), Required]
        public int ProjectAccessID { get; set; }

        [Display(Name = "Project ID"), Required]
        public int ProjectID { get; set; }

        [Display(Name = "Username"), Required]
        public string UserName { get; set; }
    }
}