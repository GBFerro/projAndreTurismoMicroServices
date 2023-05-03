using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projAndreTurismoApp.Models;

namespace projAndreTurismoApp.ClientService.Data
{
    public class projAndreTurismoAppClientServiceContext : DbContext
    {
        public projAndreTurismoAppClientServiceContext (DbContextOptions<projAndreTurismoAppClientServiceContext> options)
            : base(options)
        {
        }

        public DbSet<projAndreTurismoApp.Models.Client> Client { get; set; } = default!;
    }
}
