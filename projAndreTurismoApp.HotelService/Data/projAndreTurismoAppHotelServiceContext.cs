using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projAndreTurismoApp.Models;

namespace projAndreTurismoApp.HotelService.Data
{
    public class projAndreTurismoAppHotelServiceContext : DbContext
    {
        public projAndreTurismoAppHotelServiceContext (DbContextOptions<projAndreTurismoAppHotelServiceContext> options)
            : base(options)
        {
        }

        public DbSet<projAndreTurismoApp.Models.Hotel> Hotel { get; set; } = default!;
    }
}
