using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projAndreTurismoApp.Models;
using projAndreTurismoApp.PackageService.Data;

namespace projAndreTurismoApp.PackageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly projAndreTurismoAppPackageServiceContext _context;

        public PackagesController(projAndreTurismoAppPackageServiceContext context)
        {
            _context = context;
        }

        // GET: api/Packages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Package>>> GetPackage()
        {
            if (_context.Package == null)
            {
                return NotFound();
            }
            return await _context.Package.Include(p => p.Hotel.Address.City).Include(p => p.Client.Address.City)
                .Include(p => p.Ticket.Departure.City).Include(p => p.Ticket.Arrival.City).ToListAsync();
        }

        // GET: api/Packages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Package>> GetPackage(int id)
        {
            if (_context.Package == null)
            {
                return NotFound();
            }
            var package = await _context.Package.Include(p => p.Hotel.Address.City).Include(p => p.Client.Address.City)
                .Include(p => p.Ticket.Departure.City).Include(p => p.Ticket.Arrival.City).Where(p => p.Id == id).FirstOrDefaultAsync();

            if (package == null)
            {
                return NotFound();
            }

            return package;
        }

        // PUT: api/Packages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackage(int id, Package package)
        {
            if (id != package.Id)
            {
                return BadRequest();
            }

            _context.Entry(package).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Packages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Package>> PostPackage(Package package)
        {
            if (_context.Package == null)
            {
                return Problem("Entity set 'projAndreTurismoAppPackageServiceContext.Package'  is null.");
            }

            if (package.Id != 0)
                package.Id = 0;

            if (package.Hotel.Id != 0)
                package.Hotel.Id = 0;

            if (package.Hotel.Address.Id != 0)
                package.Hotel.Address.Id = 0;

            if (package.Hotel.Address.City.Id != 0)
                package.Hotel.Address.City.Id = 0;

            if (package.Client.Id != 0)
                package.Client.Id = 0;

            if (package.Client.Address.Id != 0)
                package.Client.Address.Id = 0;

            if (package.Client.Address.City.Id != 0)
                package.Client.Address.City.Id = 0;

            if (package.Ticket.Id != 0)
                package.Ticket.Id = 0;

            if (package.Ticket.Departure.Id != 0)
                package.Ticket.Departure.Id = 0;

            if (package.Ticket.Arrival.City.Id != 0)
                package.Ticket.Arrival.City.Id = 0;

            if (_context.Package.Count() != 0)
            {
                Package? packageConfirm = _context.Package.Include(p => p.Ticket.Departure.City).Include(p => p.Ticket.Arrival.City)
                    .Include(p => p.Hotel.Address.City).Include(p => p.Client.Address.City)
                .ToListAsync().Result.Where(p => ((p.Hotel == package.Hotel) && (p.Ticket == package.Ticket) && (p.Client == package.Client))).FirstOrDefault();
                if (packageConfirm != null)
                    return packageConfirm;
            }

            _context.Package.Add(package);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPackage", new { id = package.Id }, package);
        }

        // DELETE: api/Packages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            if (_context.Package == null)
            {
                return NotFound();
            }
            var package = await _context.Package.FindAsync(id);
            if (package == null)
            {
                return NotFound();
            }

            _context.Package.Remove(package);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PackageExists(int id)
        {
            return (_context.Package?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
