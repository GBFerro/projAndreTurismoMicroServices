using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projAndreTurismoApp.Models;

namespace projAndreTurismoApp.TicketService.Data
{
    public class projAndreTurismoAppTicketServiceContext : DbContext
    {
        public projAndreTurismoAppTicketServiceContext (DbContextOptions<projAndreTurismoAppTicketServiceContext> options)
            : base(options)
        {
        }

        public DbSet<projAndreTurismoApp.Models.Ticket> Ticket { get; set; } = default!;
    }
}
