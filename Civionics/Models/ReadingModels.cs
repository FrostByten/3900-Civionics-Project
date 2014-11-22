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
    /// Created by: Sanders Lee
    /// 
    /// Class: Reading
    /// 
    /// Data Members:
    ///     int ID
    ///     int SensorID
    ///     DateTime LoggedTime
    ///     int Data
    ///     Sensor sensor
    /// 
    /// Description:
    ///     Represents the Reading table,
    ///     which holds read data from a sensor
    /// </summary>
    public class Reading
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Sensor ID")]
        public int SensorID { get; set; }

        [Display(Name = "Anomaly")]
        public bool isAnomalous { get; set; }

        [Display(Name = "Time")]
        public DateTime LoggedTime { get; set; }

        [Display(Name = "Data")]
        public float Data { get; set; }

        public virtual Sensor Sensor { get; set; }
    }
}