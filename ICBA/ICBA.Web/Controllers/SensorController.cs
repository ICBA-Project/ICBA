using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICBA.Services;
using ICBA.Data;
using ICBA.Data.Models;
using Microsoft.AspNet.Identity;

namespace ICBA.Web.Controllers
{
    public class SensorController : Controller
    {
        private SensorsService sensorsService;
        private ApplicationDbContext dbContext;

        public SensorController(SensorsService sensorsService, ApplicationDbContext dbContext)
        {
            this.sensorsService = sensorsService;
            this.dbContext = dbContext;
        }

        public ActionResult WinService()
        {
            if (this.HttpContext.Request.Headers["auth-token"] == "0b3fb7f14591-cf7b-2834-d1e5-ef64c4e8")
            {
                sensorsService.WinService();
            }
            return View();
        }

        [Authorize]
        public ActionResult CreateSensor()
        {
            IEnumerable<Sensor> sensorsInDb = dbContext.Sensors.Where(e => e.OwnerId == null).ToList();
            return View(sensorsInDb);
        }

        [Authorize]
        public ActionResult OwnSensors()
        {
            string currentUserId = this.User.Identity.GetUserId();
            IEnumerable<Sensor> sensorsInDb = dbContext.Sensors.Where(e => e.OwnerId == currentUserId).ToList();
            return View(sensorsInDb);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSensor(Sensor sensor)
        {
            try
            {
                string sensorName = sensor.Url;
                Sensor sensorFromDb = dbContext.Sensors.Where(m => m.SensorName == sensorName).First();

                if (sensor.PollingInterval < sensorFromDb.PollingInterval)
                {
                    throw new Exception();
                }
                if (sensor.MinRange < sensorFromDb.MinRange || sensor.MinRange > sensorFromDb.MaxRange)
                {
                    throw new Exception();
                }
                if (sensor.MaxRange < sensorFromDb.MinRange || sensor.MaxRange > sensorFromDb.MaxRange)
                {
                    throw new Exception();
                }
                if (sensor.MaxRange < sensor.MinRange)
                {
                    throw new Exception();
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
                Sensor sensorToAdd = new Sensor
                {
                    Id = Guid.NewGuid(),
                    SensorName = sensor.SensorName,
                    Description = sensor.Description,
                    Url = sensorFromDb.Url,
                    MeasureType = sensorFromDb.MeasureType,
                    PollingInterval = sensor.PollingInterval,
                    AccessIsPublic = sensor.MeasureType == "on" ? true : false,
                    MinRange = sensor.MinRange,
                    MaxRange = sensor.MaxRange,
                    CurrentValue = currentValue,
                    LastUpdated = DateTime.Now.AddSeconds(-300),
                    OwnerId = this.User.Identity.GetUserId()
                };

                dbContext.Sensors.Add(sensorToAdd);
                dbContext.SaveChanges();

                return this.View("AddedSensor");
            }
            catch (Exception)
            {
                return this.View("FailedToAddSensor");
            }
        }

        private double GetRandomDouble(int minimum, int maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}