using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projAndreTurismoApp.AddressService.Data;
using projAndreTurismoApp.Models;
using projAndreTurismoApp.Models.DTO;
using projAndreTurismoApp.Services;

namespace projAndreTurismoApp.AddressService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly projAndreTurismoAppAddressServiceContext _context;
        private readonly PostOfficesService _postOfficesService;

        public AddressesController(projAndreTurismoAppAddressServiceContext context, PostOfficesService postOfficesService)
        {
            _context = context;
            _postOfficesService = postOfficesService;
        }

        // GET: api/Addresses
        [HttpGet(Name = "GetAddress")]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddress()
        {
            if (_context.Address == null)
            {
                return NotFound();
            }
            return await _context.Address.Include(a => a.City).ToListAsync();
        }

        // GET: api/Addresses/5
        [HttpGet("{id}", Name = "GetAddressById")]
        public async Task<ActionResult<Address>> GetAddressById(int id)
        {
            if (_context.Address == null)
            {
                return NotFound();
            }
            var address = await _context.Address.Include(a => a.City).Where(a => a.Id == id).FirstOrDefaultAsync();

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // GET: api/Addresses/cep
        [HttpGet("{cep:length(8)}", Name = "GetAddressByCEP")]
        public async Task<ActionResult<Address>> GetAddressByZip(string cep)
        {
            var address = await _postOfficesService.GetAddress(cep);

            Address addressComplete = new()
            {
                Street = address.Street,
                Neighborhood = address.Neighborhood,
                City = new City()
                {
                    Name = address.City
                },
                ZipCode = address.ZipCode
            };

            if (address == null)
            {
                return NotFound();
            }

            return addressComplete;
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, Address address)
        {
            if (id != address.Id)
            {
                return BadRequest();
            }

            _context.Entry(address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
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

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            if (_context.Address == null)
            {
                return Problem("Entity set 'projAndreTurismoAppAddressServiceContext.Address'  is null.");
            }

            if (address.Id != 0)
                address.Id = 0;

            if (address.City.Id != 0)
                address.City.Id = 0;

            var addressByCep = _postOfficesService.GetAddress(address.ZipCode).Result;

            if (addressByCep.Street != null)
            {
                var complement = address.Complement;
                address = new Address(addressByCep, address.Number);
                address.Complement = complement;
            }

            if (_context.Address.Count() != 0)
            {
                Address addressConfirm = _context.Address.Include(a => a.City).ToListAsync().Result.Where(c => c.Street == address.Street).FirstOrDefault();

                if (addressConfirm != null)
                    return addressConfirm;
            }


            _context.Address.Add(address);
            await _context.SaveChangesAsync();

            return address;
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            if (_context.Address == null)
            {
                return NotFound();
            }
            var address = await _context.Address.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Address.Remove(address);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressExists(int id)
        {
            return (_context.Address?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
