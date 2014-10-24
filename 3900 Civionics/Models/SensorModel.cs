using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3900_Civionics.Models
{
    public class Sensor
    {
        public int SensorID { get; set; }
        public int ProjectID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string UnitOfMeasure { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<Reading> Readings { get; set; }
    }
}