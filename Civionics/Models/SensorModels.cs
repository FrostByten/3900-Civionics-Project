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
    ///     string Name
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
        public int SensorID { get; set; }

        public int ProjectID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }

        [Display(Name = "Units")]
        public string UnitOfMeasure { get; set; }
        public SensorStatus Status { get; set; }
        public int MinSafeReading { get; set; }
        public int MaxSafeReading { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<Reading> Readings { get; set; }
    }
}