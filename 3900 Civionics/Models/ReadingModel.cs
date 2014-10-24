using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3900_Civionics.Models
{
    public class Reading
    {
        public int ID { get; set; }
        public int SensorID { get; set; }
        public DateTime LoggedTime { get; set; }
        public int Data { get; set; }

        public virtual Sensor Sensor { get; set; }
    }
}