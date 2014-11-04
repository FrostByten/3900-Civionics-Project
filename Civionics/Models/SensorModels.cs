using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Civionics.Models
{
    public enum SensorStatus
    {
        Safe, Warning, Alert
    }

    /// <summary>
    /// Description:
    ///     Represents the Sensor table,
    ///     which holds information on the sensors themselves
    /// 
    /// Data Members:
    ///     int SensorID
    ///     int ProjectID
    ///     string Type
    ///     string Location
    ///     string UnitOfMeasure
    ///     SensorStatus Status
    ///     int MinSafeReading
    ///     int MaxSafeReading
    ///     Project Project
    ///     ICollection Readings
    /// </summary>
    public class Sensor
    {
        [Key]
        [Display(Name = "Sensor ID")]
        public int ID { get; set; }

        [Display(Name = "Project ID")]
        public int ProjectID { get; set; }
        [Display(Name = "Site")]
        public string SiteID { get; set; }
        [Display(Name = "Type ID")]
        public int TypeID { get; set; }
        public SensorStatus Status { get; set; }
        [Display(Name = "Minimum Safe Reading")]
        public int MinSafeReading { get; set; } // Ask rishi if needs decimal
        [GreaterThan("MinSafeReading")]
        [Display(Name = "Maximum Safe Reading")]
        public int MaxSafeReading { get; set; } // ^

        public virtual SensorType Type { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<Reading> Readings { get; set; }
    }
}