﻿using ICBA.Data;
using ICBA.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ICBA.Services
{
    public class SensorsService : ISensorsService
    {
        private ApplicationDbContext dbContext;

        public SensorsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private void AddSensorToDatabase(Sensor sensor)
        {
            dbContext.Sensors.Add(sensor);
            dbContext.SaveChanges();
        }

        private ICollection<Sensor> ReadSensorsAll()
        {
            ICollection<Sensor> sensors = new List<Sensor>();
            HttpWebRequest request = WebRequest.Create("http://telerikacademy.icb.bg/api/sensor/all") as HttpWebRequest;
            request.Headers["auth-token"] = "8e4c46fe-5e1d-4382-b7fc-19541f7bf3b0";
            Stream objStream = request.GetResponse().GetResponseStream();
            StreamReader objReader = new StreamReader(objStream);
            List<SensorTemp> tempSensors = JsonConvert.DeserializeObject<List<SensorTemp>>(objReader.ReadToEnd());
            foreach (SensorTemp tempSensor in tempSensors)
            {
                sensors.Add(SensorTempToSensor(tempSensor));
            }
            return sensors;
        }

        private Sensor SensorTempToSensor(SensorTemp tempSensor)
        {
            Sensor sensor = new Sensor();
            sensor.Id = tempSensor.SensorId;
            sensor.SensorName = tempSensor.Tag;
            sensor.Description = tempSensor.Description;
            sensor.Url = "http://telerikacademy.icb.bg/api/sensor/" + tempSensor.SensorId;
            sensor.MeasureType = tempSensor.MeasureType;
            sensor.PollingInterval = tempSensor.MinPollingIntervalInSeconds;
            sensor.AccessIsPublic = false;
            if (tempSensor.Description.Contains("true or false"))
            {
                sensor.MinRange = 0;
                sensor.MaxRange = 1;
            }
            else
            {
                string[] words = tempSensor.Description.Split();
                sensor.MinRange = int.Parse(words[6]);
                sensor.MaxRange = int.Parse(words[8]);
            }
            string currentValue;
            if (sensor.MinRange == 0)
            {
                currentValue = "false";
            }
            else
            {
                currentValue = GetRandomDouble(sensor.MinRange, sensor.MaxRange + 1).ToString();
            }
            sensor.CurrentValue = currentValue;
            sensor.LastUpdated = DateTime.Now;
            sensor.OwnerId = null;
            return sensor;
        }

        private class SensorTemp
        {
            public Guid SensorId
            {
                get;
                set;
            }
            public string Tag
            {
                get;
                set;
            }
            public string Description
            {
                get;
                set;
            }
            public int MinPollingIntervalInSeconds
            {
                get;
                set;
            }
            public string MeasureType
            {
                get;
                set;
            }
            public override string ToString()
            {
                return string.Format("[Sensor: SensorId={0}, Tag={1}, Description={2}, MinPollingIntervalInSeconds={3}, MeasureType={4}]", SensorId, Tag, Description, MinPollingIntervalInSeconds, MeasureType);
            }
        }

        public void WinService()
        {
            List<Guid> sensorsInDb = dbContext.Sensors.Select(n => n.Id).ToList();
            IEnumerable<Sensor> sensors = ReadSensorsAll();
            foreach (Sensor sensor in sensors)
            {
                if (!sensorsInDb.Contains(sensor.Id))
                {
                    AddSensorToDatabase(sensor);
                }
            }





            foreach (Sensor sensor in dbContext.Sensors.ToList())
            {
                if (sensor.LastUpdated.AddSeconds(sensor.PollingInterval) < DateTime.Now)
                {
                    UpdateSensorsCurrentValue(sensor);
                }
            }
        }

        private void UpdateSensorsCurrentValue(Sensor sensor)
        {
            //update sensor.LastUpdated = DateTime.Now
            //Don't forget savechanges
        }

        private double GetRandomDouble(int minimum, int maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
