using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICBA.Data.Models
{
    public class SensorHistory
    {
        public Guid Id { get; set; }

        public int SensorId { get; set; }

        public string Value { get; set; }

        public DateTime When { get; set; }

        public virtual Sensor Sensor { get; set; }
    }
}
