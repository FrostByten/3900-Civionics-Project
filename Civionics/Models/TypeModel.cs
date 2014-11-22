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
    /// Class: SensorType
    /// 
    /// Data Members:
    ///     int ID       // database id for the type
    ///     int Type     // type for the data
    ///     string Unit  // unit for the data
    ///     
    /// Description:
    ///     Represents the SensorType table,
    ///     which is essentially a dynamic array,
    ///     that can to be changed during run time
    /// </summary>
    public class SensorType
    {
        [Key, Required]
        public int ID { get; set; }

        [Display(Name = "Type"), Required]
        public string Type { get; set; }

        [Display(Name = "Units"), Required]
        public string Units { get; set; }
    }
}