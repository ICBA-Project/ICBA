using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICBA.Services;
using ICBA.Data;
using ICBA.Data.Models;
using Microsoft.AspNet.Identity;
using Bytes2you.Validation;

namespace ICBA.Web.Controllers
{
    public class SensorController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly SensorsService sensorsService;

        public SensorController(SensorsService sensorsService, ApplicationDbContext dbContext)
        {
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();
            Guard.WhenArgument(sensorsService, "sensorsService").IsNull().Throw();

            this.sensorsService = sensorsService;
            this.dbContext = dbContext;
        }

        public ActionResult WinService()
        {
            string name = "ICB.ScheduledJob";
            SlackService.PostMessage("User " + name + " requested " + this.Request.Url + " url @" + this.HttpContext.Timestamp + ".");
            if (this.HttpContext.Request.Headers["auth-token"] == "0b3fb7f14591-cf7b-2834-d1e5-ef64c4e8")
            {
                sensorsService.WinService();
            }
            return View("WhoDoYouThinkYouAre");
        }

        [Authorize]
        public ActionResult CreateSensor()
        {
            SlackService.PostMessage("User " + this.User.Identity.Name + " requested " + this.Request.Url + " url @" + this.HttpContext.Timestamp + ".");
            IEnumerable<Sensor> sensorsInDb = dbContext.Sensors.Where(e => e.OwnerId == null).ToList();
            return View(sensorsInDb);
        }

        public ActionResult PublicSensors(string id)
        {
            //SlackService.PostMessage("User " + this.User.Identity.Name + " requested " + this.Request.Url + " url @" + this.HttpContext.Timestamp + ".");
            IEnumerable<Sensor> sensorsInDb = new List<Sensor>();
            switch (id)
            {
                case "alli":
                    sensorsInDb = dbContext.Sensors.Where(e => e.AccessIsPublic == true).ToList();
                    break;
                case "temp":
                    sensorsInDb = dbContext.Sensors.Where(e => e.AccessIsPublic == true && e.MeasureType == "°C").ToList();
                    break;
                case "humi":
                    sensorsInDb = dbContext.Sensors.Where(e => e.AccessIsPublic == true && e.MeasureType == "%").ToList();
                    break;
                case "elec":
                    sensorsInDb = dbContext.Sensors.Where(e => e.AccessIsPublic == true && e.MeasureType == "W").ToList();
                    break;
                case "occu":
                    sensorsInDb = dbContext.Sensors.Where(e => e.AccessIsPublic == true && (e.Url == "http://telerikacademy.icb.bg/api/sensor/4008e030-fd3a-4f8c-a8ca-4f7609ecdb1e" || e.Url == "http://telerikacademy.icb.bg/api/sensor/7a3b1db5-959d-46ce-82b6-517773327427")).ToList();
                    break;
                case "door":
                    sensorsInDb = dbContext.Sensors.Where(e => e.AccessIsPublic == true && (e.Url == "http://telerikacademy.icb.bg/api/sensor/a3b8a078-0409-4365-ace6-6f8b5b93d592" || e.Url == "http://telerikacademy.icb.bg/api/sensor/ec3c4770-5d57-4d81-9c83-a02140b883a1")).ToList();
                    break;
                case "nois":
                    sensorsInDb = dbContext.Sensors.Where(e => e.AccessIsPublic == true && e.MeasureType == "dB").ToList();
                    break;
                default:
                    throw new ArgumentException();
            }
            return View("SensorsDisplay", (IEnumerable<Sensor>)sensorsInDb);
        }

        [Authorize]
        public ActionResult OwnSensors()
        {
            SlackService.PostMessage("User " + this.User.Identity.Name + " requested " + this.Request.Url + " url @" + this.HttpContext.Timestamp + ".");
            string currentUserId = this.User.Identity.GetUserId();
            IEnumerable<Sensor> sensorsInDb = dbContext.Sensors.Where(e => e.OwnerId == currentUserId).ToList();
            return View("SensorsDisplay", sensorsInDb);
        }

        [Authorize]
        public ActionResult SharedSensors()
        {
            SlackService.PostMessage("User " + this.User.Identity.Name + " requested " + this.Request.Url + " url @" + this.HttpContext.Timestamp + ".");
            string currentUserId = this.User.Identity.GetUserId();
            IEnumerable<Sensor> sensorsInDb = dbContext.Users.First(e => e.Id == currentUserId).SharedWithUserSensors;
            return View("SensorsDisplay", sensorsInDb);
        }

        [Authorize]
        public ActionResult PrivateSensors()
        {
            SlackService.PostMessage("User " + this.User.Identity.Name + " requested " + this.Request.Url + " url @" + this.HttpContext.Timestamp + ".");
            string currentUserId = this.User.Identity.GetUserId();
            ICollection<Sensor> sensorsInDb = dbContext.Users.First(e => e.Id == currentUserId).SharedWithUserSensors;
            foreach (Sensor sensor in dbContext.Sensors.Where(e => e.OwnerId == currentUserId).ToList())
            {
                if (!sensorsInDb.Contains(sensor))
                {
                    sensorsInDb.Add(sensor);
                }
            }
            return View("SensorsDisplay", (IEnumerable<Sensor>)sensorsInDb);
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

                ICollection<ApplicationUser> sharedWithUsers = new List<ApplicationUser>();
                if (sensor.OwnerId != null)
                {
                    string[] sharedWithUsersString = sensor.OwnerId.Split(null);
                    foreach (string user in sharedWithUsersString)
                    {
                        sharedWithUsers.Add(dbContext.Users.Where(m => m.UserName == user).SingleOrDefault());
                    }
                }

                if (sensor.PollingInterval < sensorFromDb.PollingInterval)
                {
                    throw new ArgumentException();
                }
                if (sensor.MinRange < sensorFromDb.MinRange || sensor.MinRange > sensorFromDb.MaxRange)
                {
                    throw new ArgumentException();
                }
                if (sensor.MaxRange < sensorFromDb.MinRange || sensor.MaxRange > sensorFromDb.MaxRange)
                {
                    throw new ArgumentException();
                }
                if (sensor.MaxRange < sensor.MinRange)
                {
                    throw new ArgumentException();
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
                    CurrentValue = sensorFromDb.CurrentValue,
                    LastUpdated = DateTime.Now.AddSeconds(-300),
                    OwnerId = this.User.Identity.GetUserId(),
                    SharedWithUsers = sharedWithUsers
                };

                dbContext.Sensors.Add(sensorToAdd);
                dbContext.SaveChanges();

                return this.View("AddedSensor");
            }
            catch (ArgumentException)
            {
                return this.View("FailedToAddSensor");
            }
        }

        //[HttpPost]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditSensor(Sensor sensor)
        //{
        //    sensor.s
        //    if (this.User.Identity.Name != sensor.OwnerId)
        //    {
        //        return this.View("WhoDoYouThinkYouAre");
        //    }
        //    Sensor newSensorToAdd = new Sensor
        //    {
        //        Id = Guid.NewGuid(),
        //        SensorName = sensor.SensorName,
        //        Description = sensor.Description,
        //        Url = sensorFromDb.Url,
        //        MeasureType = sensorFromDb.MeasureType,
        //        PollingInterval = sensor.PollingInterval,
        //        AccessIsPublic = sensor.MeasureType == "on" ? true : false,
        //        MinRange = sensor.MinRange,
        //        MaxRange = sensor.MaxRange,
        //        CurrentValue = sensorFromDb.CurrentValue,
        //        LastUpdated = DateTime.Now.AddSeconds(-300),
        //        OwnerId = this.User.Identity.GetUserId(),
        //    };
        //    dbContext.Sensors.Add(sensorToAdd);
        //    dbContext.SaveChanges();

        //    return this.View("AddedSensor");
        //}

        private double GetRandomDouble(int minimum, int maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}