using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3900_Civionics.Models
{
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
    ///     int MinSafeReading
    ///     int MaxSafeReading
    ///     Project Project
    ///     ICollection Readings
    /// </summary>
    public class Sensor
    {
        public int SensorID { get; set; }
        public int ProjectID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string UnitOfMeasure { get; set; }
        public int MinSafeReading { get; set; }
        public int MaxSafeReading { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<Reading> Readings { get; set; }
    }
}