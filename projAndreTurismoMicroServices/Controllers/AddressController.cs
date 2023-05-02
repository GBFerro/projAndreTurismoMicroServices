using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using projAndreTurismoApp.Models;
using projAndreTurismoApp.Models.DTO;
using projAndreTurismoApp.Services;

namespace projAndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AddressService _addressService;
        public AddressController(AddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("{zip:length(8)}")]
        public ActionResult<Address> Get(string zip)
        {
            return _addressService.Get(zip).Result;
        }

        [HttpGet("{id}")]
        public async Task<Address> Get(int id)
        {
            return await _addressService.Get(id);
        }

        [HttpGet]
        public async Task<List<Address>> Get()
        {
            return await _addressService.Get();
        }

        [HttpPost]
        public async Task<ActionResult<Address>> Post(Address address)
        {
            return _addressService.Post(address).Result;
        }

        [HttpPost("{zip:length(8)}")]
        public async Task<ActionResult<Address>> Post(string zip, int num, string? complement)
        {
            return await _addressService.Post(zip, num, complement);
        }

        [HttpPut]
        public async Task<ActionResult<Address>> Put(int id, Address address)
        {
            return _addressService.Put(id, address).Result;
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            return _addressService.Delete(id).Result;
        }
    }
}
