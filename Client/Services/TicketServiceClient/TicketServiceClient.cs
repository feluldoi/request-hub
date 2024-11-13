using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using RequestHub.Client.Pages;
using RequestHub.Shared;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RequestHub.Client.Services.TicketServiceClient
{
    public class TicketServiceClient : ITicketServiceClient
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        private readonly ILogger<TicketServiceClient> _logger;

        public TicketServiceClient(HttpClient http, NavigationManager navigationManager, ILogger<TicketServiceClient> logger)
        {
            _http = http;
            _navigationManager = navigationManager;
            _logger = logger;
        }

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
        //public List<User> Users { get; set; } = new List<User>();



        //<----GET---->
        public async Task<List<Ticket>> GetTickets()
        {
            try
            {
                _logger.LogInformation("Fetching tickets from API");
                var result = await _http.GetFromJsonAsync<List<Ticket>>("api/ticket/tickets");
                if (result != null)
                {
                    Tickets = result;
                    _logger.LogInformation($"Received {Tickets.Count} tickets");
                    return result;
                }
                else
                {
                    _logger.LogInformation("Received null result from API");
                    throw new Exception("Failed to fetch tickets.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error fetching tickets: {ex.Message}");
                throw;
            }
        }

        public async Task<Ticket> GetSingleTicket(int id)
        {
            var result = await _http.GetFromJsonAsync<Ticket>($"api/ticket/{id}");
            if (result != null)
                return result;
            else
                throw new Exception("Single Ticket not found!");
        }



        public async Task<List<Department>> GetDepartments()
        {
            var result = await _http.GetFromJsonAsync<List<Department>>("api/ticket/departments");
            if (result != null)
                return result;
            else
                throw new Exception("Failed to fetch departments.");
        }

        public async Task<List<SiteLocation>> GetSiteLocations()
        {
            var result = await _http.GetFromJsonAsync<List<SiteLocation>>("api/ticket/sitelocations");
            if (result != null)
                return result;
            else
                throw new Exception("Failed to fetch tickets/sitelocations.");
        }


        public async Task<List<User>> GetUsers()
        {
            var result = await _http.GetFromJsonAsync<List<User>>("api/ticket/users");
            if (result != null)
                return result;
            else
                throw new Exception("Failed to fetch tickets/users.");
        }



        //<----P0ST--->
        public async Task<Ticket> CreateTicket(Ticket ticket)
        {
            //_logger.LogInformation("CreateTicket: client side calling API");
            var response = await _http.PostAsJsonAsync("api/ticket", ticket);
            var createdTicket = await response.Content.ReadFromJsonAsync<Ticket>();
            //await SetTickets(response);
            return createdTicket;
        }


        //<----PUT---->
        public async Task<Ticket> UpdateTicket(Ticket ticket)
        {
            _logger.LogInformation("Update Ticket: client side calling API");
            var response = await _http.PutAsJsonAsync($"api/ticket/{ticket.Id}", ticket);
            var updatedTicket = await response.Content.ReadFromJsonAsync<Ticket>();
            _logger.LogInformation($"Update Ticket: client side finished updating: {updatedTicket.Id}");
            return updatedTicket;
        }


        //<----DELETE--->
        public async Task<bool> DeleteTicket(int id)
        {
            _logger.LogInformation("delete ticket client side start");
            var response = await _http.DeleteAsync($"api/ticket/{id}");
            //var deletedResult = await response.Content.ReadFromJsonAsync<Ticket>();
            return response.IsSuccessStatusCode;


        }


        private async Task SetTickets(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Ticket>>();
            Tickets = response;
            _navigationManager.NavigateTo("tickets");
        }


    }
}
