using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projAndreTurismoApp.Models;

namespace projAndreTurismoApp.Services
{
    public class HotelService
    {
        static readonly string url = "";
        static readonly HttpClient client = new HttpClient();

        public async Task<Hotel> Get(int id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url + id);
                response.EnsureSuccessStatusCode();
                var hotel = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Hotel>(hotel);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<List<Hotel>> Get()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var hotel = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Hotel>>(hotel);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<ActionResult<Hotel>> Post(Hotel hotel)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(url, hotel);
                response.EnsureSuccessStatusCode();
                return hotel;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<ActionResult<Hotel>> Put(int id, Hotel hotel)
        {
            HttpResponseMessage responseGet = await client.GetAsync(url + id);
            var hotelGet = await responseGet.Content.ReadAsStringAsync();
            var hotelAux = JsonConvert.DeserializeObject<Hotel>(hotelGet);
            if (id != hotelAux.Id)
                return new NotFoundResult();

            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(url + id, hotel);
                response.EnsureSuccessStatusCode();
                return hotel;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage responseGet = await client.GetAsync(url + id);
            responseGet.EnsureSuccessStatusCode();
            var hotel = await responseGet.Content.ReadAsStringAsync();
            var hotelAux = JsonConvert.DeserializeObject<Hotel>(hotel);
            if (id != hotelAux.Id)
                return new NotFoundResult();

            try
            {
                HttpResponseMessage response = await client.DeleteAsync(url + id);
                response.EnsureSuccessStatusCode();
                return new OkResult();
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
