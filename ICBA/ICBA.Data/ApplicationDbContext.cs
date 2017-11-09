using ICBA.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICBA.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ICBA-DB", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual ICollection<Sensor> SharedWithUserSensors { get; set; }

        public virtual ICollection<Sensor> UserSensors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().
                HasMany(u => u.SharedWithUserSensors)
                .WithMany(s => s.SharedWithUsers)
                .Map(us =>
                {
                    us.MapLeftKey("UserRefId");
                    us.MapRightKey("SensorRefId");
                    us.ToTable("UserSensor");
                });

            modelBuilder.Entity<Sensor>()
                .HasMany(s => s.SensorHistory)
                .WithRequired(h => h.Sensor);
        }

        public DbSet<Sensor> Sensors { get; set; }

        public DbSet<SensorHistory> SensorHistory { get; set; }
    }
}
