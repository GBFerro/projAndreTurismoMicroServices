using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using projAndreTurismoApp.Models;
using projAndreTurismoApp.Services;

namespace projAndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CityService _cityService;

        public CityController(CityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet("{id}")]
        public async Task<City> Get(int id)
        {
            return await _cityService.Get(id);
        }

        [HttpGet]
        public async Task<List<City>> Get()
        {
            return await _cityService.Get();
        }

        [HttpPost]
        public async Task<ActionResult<City>> Post(City city)
        {
            return _cityService.Post(city).Result;
        }

        [HttpPut]
        public async Task<ActionResult<City>> Put(int id, City city)
        {
            return _cityService.Put(id, city).Result;
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            return _cityService.Delete(id).Result;
        }
    }
}
