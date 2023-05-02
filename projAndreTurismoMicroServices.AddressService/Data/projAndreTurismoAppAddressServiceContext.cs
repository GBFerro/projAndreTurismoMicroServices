using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projAndreTurismoApp.Models;

namespace projAndreTurismoApp.AddressService.Data
{
    public class projAndreTurismoAppAddressServiceContext : DbContext
    {
        public projAndreTurismoAppAddressServiceContext (DbContextOptions<projAndreTurismoAppAddressServiceContext> options)
            : base(options)
        {
        }

        public DbSet<projAndreTurismoApp.Models.Address> Address { get; set; } = default!;
    }
}
