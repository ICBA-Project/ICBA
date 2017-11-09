using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICBA.Data.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.UserSensors = new HashSet<Sensor>();
            this.SharedWithUserSensors = new HashSet<Sensor>();
        }

        public virtual ICollection<Sensor> SharedWithUserSensors { get; set; }

        public virtual ICollection<Sensor> UserSensors { get; set; }
    }
}
