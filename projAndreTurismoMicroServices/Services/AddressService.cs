using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projAndreTurismoApp.Models;
using projAndreTurismoApp.Models.DTO;

namespace projAndreTurismoApp.Services
{
    public class AddressService
    {
        static readonly string url = "https://localhost:7187/api/Addresses/";
        static readonly HttpClient client = new HttpClient();
        public async Task<Address> Get(string zip)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url+zip);
                response.EnsureSuccessStatusCode();
                var address = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Address>(address);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Address> Get(int id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url + id);
                response.EnsureSuccessStatusCode();
                var address = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Address>(address);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<List<Address>> Get()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var address = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Address>>(address);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<ActionResult<Address>> Post(Address address)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(url, address);
                response.EnsureSuccessStatusCode();
                return address;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<ActionResult<Address>> Post(string zip, int num, string? complement)
        {
            try
            {
                HttpResponseMessage responseGet = await client.GetAsync(url + zip);
                responseGet.EnsureSuccessStatusCode();
                var addressJson = await responseGet.Content.ReadAsStringAsync();
                Address address = JsonConvert.DeserializeObject<Address>(addressJson);

                address.Number = num;
                address.City.RegisterDate = DateTime.Now;
                address.RegisterDate = DateTime.Now;
                address.Complement = complement;

                HttpResponseMessage responsePost = await client.PostAsJsonAsync(url, address);
                responsePost.EnsureSuccessStatusCode();
                return address;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<ActionResult<Address>> Put(int id, Address address)
        {
            HttpResponseMessage responseGet = await client.GetAsync(url + id);
            var addressGet = await responseGet.Content.ReadAsStringAsync();
            var addressAux = JsonConvert.DeserializeObject<Address>(addressGet);
            if (id != addressAux.Id)
                return new NotFoundResult();

            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(url + id, address);
                response.EnsureSuccessStatusCode();
                return address;
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
            var address = await responseGet.Content.ReadAsStringAsync();
            var addressAux = JsonConvert.DeserializeObject<Address>(address);
            if (id != addressAux.Id)
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
