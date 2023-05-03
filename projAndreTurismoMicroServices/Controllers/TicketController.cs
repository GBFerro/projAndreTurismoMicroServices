using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using projAndreTurismoApp.Models;
using projAndreTurismoApp.Services;

namespace projAndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly TicketService _ticketService;
        private readonly HotelService _clientService;
        private readonly AddressService _addressService;
        private readonly CityService _cityService;
        public TicketController(AddressService addressService, CityService cityService, HotelService clientService, TicketService ticketService)
        {
            _addressService = addressService;
            _cityService = cityService;
            _clientService = clientService;
            _ticketService = ticketService;
        }

        [HttpGet("{id}")]
        public async Task<Ticket> Get(int id)
        {
            return await _ticketService.Get(id);
        }

        [HttpGet]
        public async Task<List<Ticket>> Get()
        {
            return await _ticketService.Get();
        }

        [HttpPost]
        public async Task<ActionResult<Ticket>> Post(Ticket ticket)
        {
            return _ticketService.Post(ticket).Result;
        }

        [HttpPut]
        public async Task<ActionResult<Ticket>> Put(int id, Ticket ticket)
        {
            return _ticketService.Put(id, ticket).Result;
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            Ticket ticket = _ticketService.Get(id).Result;

            Address arrivalConfirm = ticket.Arrival;
            if (arrivalConfirm.Street != null)
                _addressService.Delete(arrivalConfirm.Id);

            City cityAConfirm = ticket.Arrival.City;
            if (cityAConfirm.Name != null)
                _cityService.Delete(cityAConfirm.Id);

            Address departureConfirm = ticket.Departure;
            if (departureConfirm.Street != null)
                _addressService.Delete(departureConfirm.Id);

            City cityDConfirm = ticket.Departure.City;
            if (cityDConfirm.Name != null)
                _cityService.Delete(cityDConfirm.Id);

            return _ticketService.Delete(id).Result;
        }
    }
}
