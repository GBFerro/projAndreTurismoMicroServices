using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projAndreTurismoApp.Models;

namespace projAndreTurismoApp.Services
{
    public class TicketService
    {
        static readonly string url = "https://localhost:7242/api/Tickets/";
        static readonly HttpClient client = new HttpClient();

        public async Task<Ticket> Get(int id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url + id);
                response.EnsureSuccessStatusCode();
                var ticket = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Ticket>(ticket);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<List<Ticket>> Get()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var ticket = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Ticket>>(ticket);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<ActionResult<Ticket>> Post(Ticket ticket)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(url, ticket);
                response.EnsureSuccessStatusCode();
                return ticket;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<ActionResult<Ticket>> Put(int id, Ticket ticket)
        {
            HttpResponseMessage responseGet = await client.GetAsync(url + id);
            var ticketGet = await responseGet.Content.ReadAsStringAsync();
            var ticketAux = JsonConvert.DeserializeObject<Ticket>(ticketGet);
            if (id != ticketAux.Id)
                return new NotFoundResult();

            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(url + id, ticket);
                response.EnsureSuccessStatusCode();
                return ticket;
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
            var ticket = await responseGet.Content.ReadAsStringAsync();
            var ticketAux = JsonConvert.DeserializeObject<Ticket>(ticket);
            if (id != ticketAux.Id)
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
