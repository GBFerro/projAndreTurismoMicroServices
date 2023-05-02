using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projAndreTurismoApp.Models;

namespace projAndreTurismoApp.Services
{
    public class CityService
    {
        static readonly string url = "https://localhost:7073/api/Cities/";
        static readonly HttpClient client = new HttpClient();

        public async Task<List<City>> Get()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string city = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<City>>(city);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<City> Get(int id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url + id);
                response.EnsureSuccessStatusCode();
                string city = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<City>(city);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<ActionResult<City>> Post(City city)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(url, city);
                response.EnsureSuccessStatusCode();
                return city;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<ActionResult<City>> Put(int id, City city)
        {
            HttpResponseMessage responseGet = await client.GetAsync(url + id);
            var cityGet = await responseGet.Content.ReadAsStringAsync();
            var cityAux = JsonConvert.DeserializeObject<Address>(cityGet);
            if (id != cityAux.Id)
                return new NotFoundResult();

            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(url+id, city);
                response.EnsureSuccessStatusCode();
                return city;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage responseGet = await client.GetAsync(url + id);
            var cityGet = await responseGet.Content.ReadAsStringAsync();
            var cityAux = JsonConvert.DeserializeObject<Address>(cityGet);
            if (id != cityAux.Id)
                return new NotFoundResult();

            try
            {
                HttpResponseMessage response = await client.DeleteAsync(url+id);
                response.EnsureSuccessStatusCode();
                return new OkResult();
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }


    }
}

