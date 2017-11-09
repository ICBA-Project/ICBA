using ICBA.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICBA.Services
{
    public interface ISensorsService
    {
        ICollection<Sensor> ReadSensorsAll();
    }
}
