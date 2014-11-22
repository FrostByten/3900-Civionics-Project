using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Civionics.Models
{
    /// <summary>
    /// File: ReadingModels.cs
    /// 
    /// Designed by: Michael Chimick
    /// Created by: Sanders Lee
    /// Finished by: Michael Chimick
    /// Edited by: Lewis Scott
    ///            Michael Chimick
    /// 
    /// Class: Reading
    /// 
    /// Data Members:
    ///     int ID               // database id for a reading
    ///     int SensorID         // id tying the reading to a Sensor
    ///     bool isAnomalous     // status; determined by data, effects Sensor status
    ///     DateTime LoggedTime  // time the reading was taken
    ///     float Data           // data value of the reading
    ///     
    ///     Sensor sensor        // virtual Sensor object
    /// 
    /// Description:
    ///     Represents the Reading table,
    ///     which holds read data from a sensor
    /// </summary>
    public class Reading
    {
        [Key, Required]
        public int ID { get; set; }

        [Display(Name = "Sensor ID"), Required]
        public int SensorID { get; set; }

        [Display(Name = "Anomaly")]
        public bool isAnomalous { get; set; }

        [Display(Name = "Time"), Required]
        public DateTime LoggedTime { get; set; }

        [Display(Name = "Data"), Required]
        public float Data { get; set; }

        public virtual Sensor Sensor { get; set; }
    }
}