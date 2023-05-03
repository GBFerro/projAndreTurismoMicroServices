using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using projAndreTurismoApp.Models;
using projAndreTurismoApp.Services;

namespace projAndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService;
        private readonly AddressService _addressService;
        private readonly CityService _cityService;
        public ClientController(AddressService addressService, CityService cityService, ClientService clientService)
        {
            _addressService = addressService;
            _cityService = cityService;
            _clientService = clientService;
        }

        [HttpGet("{id}")]
        public async Task<Client> Get(int id)
        {
            return await _clientService.Get(id);
        }

        [HttpGet]
        public async Task<List<Client>> Get()
        {
            return await _clientService.Get();
        }

        [HttpPost]
        public async Task<ActionResult<Client>> Post(Client client)
        {
            return _clientService.Post(client).Result;
        }

        [HttpPut]
        public async Task<ActionResult<Client>> Put(int id, Client client)
        {
            return _clientService.Put(id, client).Result;
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            Client client = Get(id).Result;

            Address addressConfirm = client.Address;
            if (addressConfirm.Street != null)
                _addressService.Delete(addressConfirm.Id);

            City cityConfirm = client.Address.City;
            if (cityConfirm.Name != null)
                _cityService.Delete(cityConfirm.Id);

            return _clientService.Delete(id).Result;
        }
    }
}
