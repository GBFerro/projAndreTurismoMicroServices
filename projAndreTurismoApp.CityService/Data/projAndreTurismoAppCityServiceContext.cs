using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projAndreTurismoApp.Models;

namespace projAndreTurismoApp.CityService.Data
{
    public class projAndreTurismoAppCityServiceContext : DbContext
    {
        public projAndreTurismoAppCityServiceContext (DbContextOptions<projAndreTurismoAppCityServiceContext> options)
            : base(options)
        {
        }

        public DbSet<projAndreTurismoApp.Models.City> City { get; set; } = default!;
    }
}
