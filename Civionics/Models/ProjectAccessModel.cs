using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Civionics.Models
{
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
        [Display(Name = "ID")]
        public int ProjectAccessID { get; set; }

        [Display(Name = "Project ID")]
        public int ProjectID { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }
    }
}