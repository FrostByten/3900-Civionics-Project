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
                new Sensor{SensorID=0,ProjectID=0,Name="TestSensor1",Type="1-Axis Accelerometer",Location="Here",UnitOfMeasure="Gs",MinSafeReading=1,MaxSafeReading=5},
                new Sensor{SensorID=1,ProjectID=0,Name="TestSensor2",Type="3-Axis Accelerometer",Location="Here",UnitOfMeasure="Gs",MinSafeReading=0,MaxSafeReading=40},
                new Sensor{SensorID=2,ProjectID=0,Name="TestSensor3",Type="1-Axis Accelerometer",Location="Here",UnitOfMeasure="Gs",MinSafeReading=5,MaxSafeReading=10},
                new Sensor{SensorID=3,ProjectID=0,Name="TestSensor4",Type="2-Axis Accelerometer",Location="Here",UnitOfMeasure="Gs",MinSafeReading=6,MaxSafeReading=11},
                new Sensor{SensorID=4,ProjectID=1,Name="TestSensor5",Type="1-Axis Accelerometer",Location="Here",UnitOfMeasure="Gs",MinSafeReading=0,MaxSafeReading=2},
                new Sensor{SensorID=5,ProjectID=1,Name="TestSensor6",Type="3-Axis Accelerometer",Location="Here",UnitOfMeasure="Gs",MinSafeReading=20,MaxSafeReading=60},
                new Sensor{SensorID=6,ProjectID=1,Name="TestSensor7",Type="1-Axis Accelerometer",Location="Here",UnitOfMeasure="Gs",MinSafeReading=8,MaxSafeReading=400},
                new Sensor{SensorID=7,ProjectID=1,Name="TestSensor8",Type="2-Axis Accelerometer",Location="Here",UnitOfMeasure="Gs",MinSafeReading=1,MaxSafeReading=6},
                new Sensor{SensorID=8,ProjectID=2,Name="TestSensor9",Type="1-Axis Accelerometer",Location="Here",UnitOfMeasure="Gs",MinSafeReading=0,MaxSafeReading=1},
                new Sensor{SensorID=9,ProjectID=2,Name="TestSensor10",Type="Corrosion",Location="Here",UnitOfMeasure="Degrees C",MinSafeReading=-2,MaxSafeReading=69},
                new Sensor{SensorID=10,ProjectID=2,Name="TestSensor11",Type="1-Axis Accelerometer",Location="Here",UnitOfMeasure="Gs",MinSafeReading=3,MaxSafeReading=4},
                new Sensor{SensorID=11,ProjectID=2,Name="TestSensor12",Type="2-Axis Accelerometer",Location="Here",UnitOfMeasure="Gs",MinSafeReading=-6,MaxSafeReading=3},
                new Sensor{SensorID=12,ProjectID=3,Name="TestSensor13",Type="Corriosion",Location="Here",UnitOfMeasure="Degrees F",MinSafeReading=-40,MaxSafeReading=200},
                new Sensor{SensorID=13,ProjectID=3,Name="TestSensor14",Type="1-Axis Accelerometer",Location="Here",UnitOfMeasure="Gs",MinSafeReading=2,MaxSafeReading=4},
                new Sensor{SensorID=14,ProjectID=3,Name="TestSensor15",Type="1-Axis Accelerometer",Location="Here",UnitOfMeasure="Gs",MinSafeReading=0,MaxSafeReading=45},
                new Sensor{SensorID=15,ProjectID=3,Name="TestSensor16",Type="3-Axis Accelerometer",Location="Here",UnitOfMeasure="Gs",MinSafeReading=-20,MaxSafeReading=74},
                new Sensor{SensorID=16,ProjectID=4,Name="TestSensor17",Type="1-Axis Accelerometer",Location="Here",UnitOfMeasure="Gs",MinSafeReading=1,MaxSafeReading=8},
                new Sensor{SensorID=17,ProjectID=4,Name="TestSensor18",Type="1-Axis Accelerometer",Location="Here",UnitOfMeasure="Gs",MinSafeReading=6,MaxSafeReading=10},
                new Sensor{SensorID=18,ProjectID=4,Name="TestSensor19",Type="Corrosion",Location="Here",UnitOfMeasure="Degrees F",MinSafeReading=-200,MaxSafeReading=102},
                new Sensor{SensorID=19,ProjectID=4,Name="TestSensor20",Type="Corrosion",Location="Here",UnitOfMeasure="Degrees C",MinSafeReading=-2,MaxSafeReading=55}
            };
            sensors.ForEach(s => context.Sensors.Add(s));
            context.SaveChanges();
        }
    }
}