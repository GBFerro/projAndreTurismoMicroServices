using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using projAndreTurismoApp.Models;
using projAndreTurismoApp.Services;

namespace projAndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly HotelService _hotelService;
        private readonly AddressService _addressService;
        private readonly CityService _cityService;
        public HotelController(AddressService addressService, CityService cityService, HotelService hotelService)
        {
            _addressService = addressService;
            _cityService = cityService;
            _hotelService = hotelService;
        }

        [HttpGet("{id}")]
        public async Task<Hotel> Get(int id)
        {
            return await _hotelService.Get(id);
        }

        [HttpGet]
        public async Task<List<Hotel>> Get()
        {
            return await _hotelService.Get();
        }

        [HttpPost]
        public async Task<ActionResult<Hotel>> Post(Hotel hotel)
        {
            return _hotelService.Post(hotel).Result;
        }

        [HttpPut]
        public async Task<ActionResult<Hotel>> Put(int id, Hotel hotel)
        {
            return _hotelService.Put(id, hotel).Result;
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            Hotel hotel = _hotelService.Get(id).Result;

            Address addressConfirm = hotel.Address;
            if (addressConfirm.Street != null)
                _addressService.Delete(addressConfirm.Id);

            City cityConfirm = hotel.Address.City;
            if (cityConfirm.Name != null)
                _cityService.Delete(cityConfirm.Id);

            return _hotelService.Delete(id).Result;
        }
    }
}
