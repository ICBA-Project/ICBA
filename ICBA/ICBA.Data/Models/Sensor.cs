using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICBA.Data.Models
{
    public class Sensor
    {
        public Sensor()
        {
            this.SharedWithUsers = new HashSet<User>();
            this.SensorHistory = new HashSet<SensorHistory>();
        }

        public Guid Id { get; set; }

        public string SensorName { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string MeasureType { get; set; }

        public int PollingInterval { get; set; }

        public bool AccessIsPublic { get; set; }

        public int MinRange { get; set; }

        public int MaxRange { get; set; }

        public DateTime LastUpdated { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public virtual ICollection<User> SharedWithUsers { get; set; }

        public virtual User Owner { get; set; }

        public virtual ICollection<SensorHistory> SensorHistory { get; set; }
    }
}
