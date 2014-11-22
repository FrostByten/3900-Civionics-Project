using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Civionics.Models
{
    // Enum defining the states a sensor can have
    public enum SensorStatus
    {
        Safe, Warning, Alert
    }

    /// <summary>
    /// File: SensorModels.cs
    /// 
    /// Designed by: Michael Chimick
    /// Created by: Sanders Lee
    /// Finished by: Michael Chimick
    /// Edited by: Lewis Scott
    ///            Michael Chimick
    /// 
    /// Class: Sensor
    /// 
    /// Data Members:
    ///     int ID                // database id for the sensor
    ///     int ProjectID         // id tying a sensor to a Project
    ///     int TypeID            // id tying a sensor to a SensorType
    ///     string SiteID         // id of where the sensor is physically located
    ///     SensorStatus Status   // status object; determined by readings, effects project status
    ///     float MinSafeReading  // lower bound of the safe range for readings
    ///     float MaxSafeReading  // upper bound of the safe range for readings
    ///     bool AutoRange        // determines if the auto range-tweaking system will be used
    ///     bool AutoPercent      // determines if the auto percent-tweaking system will be used
    ///     string Serial         // serial number of the physical sensor
    ///     
    ///     SensorType Type       // virtual SensorType object
    ///     Project Project       // virtual Project object
    ///     ICollection Readings  // virtual Collection of Reading objects
    /// 
    /// Description:
    ///     Represents the Sensor table,
    ///     which holds information on the sensor devices
    /// </summary>
    public class Sensor
    {
        [Key, Display(Name = "Sensor ID"), Required]
        public int ID { get; set; }

        [Display(Name = "Project ID"), Required]
        public int ProjectID { get; set; }
        [Display(Name = "Site"), Required]
        public string SiteID { get; set; }
        [Display(Name = "Type ID"), Required]
        public int TypeID { get; set; }
        public SensorStatus Status { get; set; }
        [Display(Name = "Minimum Safe Reading"), Required]
        public float MinSafeReading { get; set; } 
        [Display(Name = "Maximum Safe Reading"), Required]
        public float MaxSafeReading { get; set; } 
        [Display(Name = "Auto-Range"), Required]
        public bool AutoRange { get; set; }
        [Display(Name = "Auto Percent"), Required]
        public int AutoPercent { get; set; }
        [Display(Name = "Serial #"), Required]
        public string serial { get; set; }

        public virtual SensorType Type { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<Reading> Readings { get; set; }
    }
}