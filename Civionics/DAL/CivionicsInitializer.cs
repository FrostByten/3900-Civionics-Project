using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

using Civionics.Models;

namespace Civionics.DAL
{
    public class CivionicsInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CivionicsContext>
    {
        protected override void Seed(CivionicsContext context)
        {
            var units = new List<Unit>
            {
                new Unit{UnitID="Gs"},
                new Unit{UnitID="Volts"},
                new Unit{UnitID="Celcius"}
            };
            units.ForEach(p => context.Units.Add(p));
            context.SaveChanges();

            var types = new List<SensorType>
            {
                new SensorType{SensorTypeID="X-Axis Accelerometer"},
                new SensorType{SensorTypeID="Y-Axis Accelerometer"},
                new SensorType{SensorTypeID="Z-Axis Accelerometer"},
                new SensorType{SensorTypeID="Corrosion"}
            };
            units.ForEach(p => context.Units.Add(p));
            context.SaveChanges();

            var projects = new List<Project>
            {
                new Project{ID=0,Name="TestProject1",Description="Test project 1", DateAdded=DateTime.Parse("2014-10-31")},
                new Project{ID=1,Name="TestProject2",Description="Test project 2", DateAdded=DateTime.Parse("2014-09-02")},
                new Project{ID=2,Name="TestProject3",Description="Test project 3", DateAdded=DateTime.Parse("2014-08-30")},
                new Project{ID=3,Name="TestProject4",Description="Test project 4", DateAdded=DateTime.Parse("2014-01-14")},
                new Project{ID=4,Name="TestProject5",Description="Test project 5", DateAdded=DateTime.Parse("2014-10-31")}
            };
            projects.ForEach(p => context.Projects.Add(p));
            context.SaveChanges();

            var sensors = new List<Sensor>
            {
                new Sensor{ID=0,ProjectID=0,SensorTypeID="Z-Axis Accelerometer",SiteID="Here",UnitID="Gs",MinSafeReading=1,MaxSafeReading=5},
                new Sensor{ID=1,ProjectID=0,SensorTypeID="Y-Axis Accelerometer",SiteID="Here",UnitID="Gs",MinSafeReading=0,MaxSafeReading=40},
                new Sensor{ID=2,ProjectID=0,SensorTypeID="Z-Axis Accelerometer",SiteID="Here",UnitID="Gs",MinSafeReading=5,MaxSafeReading=10},
                new Sensor{ID=3,ProjectID=0,SensorTypeID="X-Axis Accelerometer",SiteID="Here",UnitID="Gs",MinSafeReading=6,MaxSafeReading=11},
                new Sensor{ID=4,ProjectID=1,SensorTypeID="Z-Axis Accelerometer",SiteID="Here",UnitID="Gs",MinSafeReading=0,MaxSafeReading=2},
                new Sensor{ID=5,ProjectID=1,SensorTypeID="Y-Axis Accelerometer",SiteID="Here",UnitID="Gs",MinSafeReading=20,MaxSafeReading=60},
                new Sensor{ID=6,ProjectID=1,SensorTypeID="Z-Axis Accelerometer",SiteID="Here",UnitID="Gs",MinSafeReading=8,MaxSafeReading=400},
                new Sensor{ID=7,ProjectID=1,SensorTypeID="X-Axis Accelerometer",SiteID="Here",UnitID="Gs",MinSafeReading=1,MaxSafeReading=6},
                new Sensor{ID=8,ProjectID=2,SensorTypeID="Z-Axis Accelerometer",SiteID="Here",UnitID="Gs",MinSafeReading=0,MaxSafeReading=1},
                new Sensor{ID=9,ProjectID=2,SensorTypeID="Corrosion",SiteID="Here",UnitID="Degrees C",MinSafeReading=-2,MaxSafeReading=69},
                new Sensor{ID=10,ProjectID=2,SensorTypeID="Z-Axis Accelerometer",SiteID="Here",UnitID="Gs",MinSafeReading=3,MaxSafeReading=4},
                new Sensor{ID=11,ProjectID=2,SensorTypeID="X-Axis Accelerometer",SiteID="Here",UnitID="Gs",MinSafeReading=-6,MaxSafeReading=3},
                new Sensor{ID=12,ProjectID=3,SensorTypeID="Corriosion",SiteID="Here",UnitID="Degrees F",MinSafeReading=-40,MaxSafeReading=200},
                new Sensor{ID=13,ProjectID=3,SensorTypeID="Z-Axis Accelerometer",SiteID="Here",UnitID="Gs",MinSafeReading=2,MaxSafeReading=4},
                new Sensor{ID=14,ProjectID=3,SensorTypeID="Z-Axis Accelerometer",SiteID="Here",UnitID="Gs",MinSafeReading=0,MaxSafeReading=45},
                new Sensor{ID=15,ProjectID=3,SensorTypeID="Y-Axis Accelerometer",SiteID="Here",UnitID="Gs",MinSafeReading=-20,MaxSafeReading=74},
                new Sensor{ID=16,ProjectID=4,SensorTypeID="Z-Axis Accelerometer",SiteID="Here",UnitID="Gs",MinSafeReading=1,MaxSafeReading=8},
                new Sensor{ID=17,ProjectID=4,SensorTypeID="Z-Axis Accelerometer",SiteID="Here",UnitID="Gs",MinSafeReading=6,MaxSafeReading=10},
                new Sensor{ID=18,ProjectID=4,SensorTypeID="Corrosion",SiteID="Here",UnitID="Degrees F",MinSafeReading=-200,MaxSafeReading=102},
                new Sensor{ID=19,ProjectID=4,SensorTypeID="Corrosion",SiteID="Here",UnitID="Degrees C",MinSafeReading=-2,MaxSafeReading=55}
            };
            sensors.ForEach(s => context.Sensors.Add(s));
            context.SaveChanges();
        }
    }
}