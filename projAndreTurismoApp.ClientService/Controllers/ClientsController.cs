using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projAndreTurismoApp.ClientService.Data;
using projAndreTurismoApp.Models;

namespace projAndreTurismoApp.ClientService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly projAndreTurismoAppClientServiceContext _context;

        public ClientsController(projAndreTurismoAppClientServiceContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClient()
        {
            if (_context.Client == null)
            {
                return NotFound();
            }
            return await _context.Client.Include(c => c.Address.City).ToListAsync();
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            if (_context.Client == null)
            {
                return NotFound();
            }
            var client = await _context.Client.Include(c => c.Address.City).Where(cl => cl.Id == id).FirstOrDefaultAsync();

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            if (_context.Client == null)
            {
                return Problem("Entity set 'projAndreTurismoAppClientServiceContext.Client'  is null.");
            }

            if (client.Id != 0)
                client.Id = 0;

            if (client.Address.Id != 0)
                client.Address.Id = 0;

            if (client.Address.City.Id != 0)
                client.Address.City.Id = 0;

            if (_context.Client.Count() != 0)
            {
                Client? ClientConfirm = _context.Client.Include(c => c.Address.City).ToListAsync().Result.Where(c => c.Name == client.Name).FirstOrDefault();

                if (ClientConfirm != null)
                    return ClientConfirm;
            }

            _context.Client.Add(client);
            await _context.SaveChangesAsync();

            return client;
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            if (_context.Client == null)
            {
                return NotFound();
            }
            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Client.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(int id)
        {
            return (_context.Client?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
