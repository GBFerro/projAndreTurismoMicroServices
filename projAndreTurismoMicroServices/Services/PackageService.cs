using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projAndreTurismoApp.Models;

namespace projAndreTurismoApp.Services
{
    public class PackageService
    {
        static readonly string url = "";
        static readonly HttpClient client = new HttpClient();

        public async Task<Package> Get(int id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url + id);
                response.EnsureSuccessStatusCode();
                var package = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Package>(package);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<List<Package>> Get()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var package = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Package>>(package);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<ActionResult<Package>> Post(Package package)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(url, package);
                response.EnsureSuccessStatusCode();
                return package;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<ActionResult<Package>> Put(int id, Package package)
        {
            HttpResponseMessage responseGet = await client.GetAsync(url + id);
            var packageGet = await responseGet.Content.ReadAsStringAsync();
            var packageAux = JsonConvert.DeserializeObject<Package>(packageGet);
            if (id != packageAux.Id)
                return new NotFoundResult();

            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(url + id, package);
                response.EnsureSuccessStatusCode();
                return package;
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
            var package = await responseGet.Content.ReadAsStringAsync();
            var packageAux = JsonConvert.DeserializeObject<Package>(package);
            if (id != packageAux.Id)
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
