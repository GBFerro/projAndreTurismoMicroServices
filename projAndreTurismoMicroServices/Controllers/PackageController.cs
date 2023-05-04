using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using projAndreTurismoApp.Models;
using projAndreTurismoApp.Services;

namespace projAndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly PackageService _packageService;
        private readonly ClientService _clientService;
        private readonly HotelService _hotelService;
        private readonly TicketService _ticketService;
        private readonly AddressService _addressService;
        private readonly CityService _cityService;
        public PackageController(AddressService addressService, CityService cityService, ClientService clientService, TicketService ticketService, HotelService hotelService, PackageService packageService)
        {
            _addressService = addressService;
            _cityService = cityService;
            _ticketService = ticketService;
            _hotelService = hotelService;
            _clientService = clientService;
            _packageService = packageService;
        }

        [HttpGet("{id}")]
        public async Task<Package> Get(int id)
        {
            return await _packageService.Get(id);
        }

        [HttpGet]
        public async Task<List<Package>> Get()
        {
            return await _packageService.Get();
        }

        [HttpPost]
        public async Task<ActionResult<Package>> Post(Package package)
        {
            return _packageService.Post(package).Result;
        }

        [HttpPut]
        public async Task<ActionResult<Package>> Put(int id, Package package)
        {
            return _packageService.Put(id, package).Result;
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            Package package = _packageService.Get(id).Result;

            Hotel hotelConfirm = package.Hotel;
            if (hotelConfirm != null)
                _ticketService.Delete(hotelConfirm.Id);

            Address addressHConfirm = package.Hotel.Address;
            if (addressHConfirm.Street != null)
                _addressService.Delete(addressHConfirm.Id);

            City cityHConfirm = package.Hotel.Address.City;
            if (cityHConfirm.Name != null)
                _cityService.Delete(cityHConfirm.Id);

            Client clientConfirm = package.Client;
            if (clientConfirm != null)
                _clientService.Delete(clientConfirm.Id);

            Address addressCConfirm = package.Client.Address;
            if (addressCConfirm.Street != null)
                _addressService.Delete(addressCConfirm.Id);

            City cityCConfirm = package.Client.Address.City;
            if (cityCConfirm.Name != null)
                _cityService.Delete(cityCConfirm.Id);

            Ticket ticketConfirm = package.Ticket;
            if (ticketConfirm != null)
                _ticketService.Delete(ticketConfirm.Id);

            Address arrivalConfirm = package.Ticket.Arrival;
            if (arrivalConfirm.Street != null)
                _addressService.Delete(arrivalConfirm.Id);

            City cityAConfirm = package.Ticket.Arrival.City;
            if (cityAConfirm.Name != null)
                _cityService.Delete(cityAConfirm.Id);

            Address departureConfirm = package.Ticket.Departure;
            if (departureConfirm.Street != null)
                _addressService.Delete(departureConfirm.Id);

            City cityDConfirm = package.Ticket.Departure.City;
            if (cityDConfirm.Name != null)
                _cityService.Delete(cityDConfirm.Id);

            return _packageService.Delete(id).Result;
        }
    }
}
