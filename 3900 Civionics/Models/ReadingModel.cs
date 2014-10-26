using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3900_Civionics.Models
{
    /// <summary>
    /// Description:
    ///     Represents the Reading table,
    ///     which holds read data from a sensor
    /// 
    /// Data Members:
    ///     int ID
    ///     int SensorID
    ///     DateTime LoggedTime
    ///     int Data
    ///     Sensor sensor
    /// </summary>
    public class Reading // NOTE to self; refactor class
    {
        public int ID { get; set; }
        public int SensorID { get; set; }
        public DateTime LoggedTime { get; set; }
        public int Data { get; set; }

        public virtual Sensor Sensor { get; set; }
    }
}