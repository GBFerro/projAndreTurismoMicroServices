using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projAndreTurismoApp.Models;

namespace projAndreTurismoApp.PackageService.Data
{
    public class projAndreTurismoAppPackageServiceContext : DbContext
    {
        public projAndreTurismoAppPackageServiceContext (DbContextOptions<projAndreTurismoAppPackageServiceContext> options)
            : base(options)
        {
        }

        public DbSet<projAndreTurismoApp.Models.Package> Package { get; set; } = default!;
    }
}
