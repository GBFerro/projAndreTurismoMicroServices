using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projAndreTurismoApp.Models;

namespace projAndreTurismoApp.Services
{
    public class ClientService
    {
        static readonly string url = "https://localhost:7237/api/Clients/";
        static readonly HttpClient client = new HttpClient();

        public async Task<Client> Get(int id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url + id);
                response.EnsureSuccessStatusCode();
                var customer = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Client>(customer);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<List<Client>> Get()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var customer = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Client>>(customer);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<ActionResult<Client>> Post(Client customer)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(url, customer);
                response.EnsureSuccessStatusCode();
                return customer;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<ActionResult<Client>> Put(int id, Client customer)
        {
            HttpResponseMessage responseGet = await client.GetAsync(url + id);
            var customerGet = await responseGet.Content.ReadAsStringAsync();
            var customerAux = JsonConvert.DeserializeObject<Client>(customerGet);
            if (id != customerAux.Id)
                return new NotFoundResult();

            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(url + id, customer);
                response.EnsureSuccessStatusCode();
                return customer;
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
            var customer = await responseGet.Content.ReadAsStringAsync();
            var customerAux = JsonConvert.DeserializeObject<Client>(customer);
            if (id != customerAux.Id)
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
