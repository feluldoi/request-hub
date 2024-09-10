using RequestHub.Shared;

namespace RequestHub.Client.Services.TicketServiceClient
{
    public interface ITicketServiceClient
    {
        List<Ticket> Tickets { get; set; }

        Task<List<Ticket>> GetTickets();
        Task<Ticket> GetSingleTicket(int id);
        Task<List<Department>> GetDepartments();
        Task<List<SiteLocation>> GetSiteLocations();
        Task<List<User>> GetUsers();
        Task<Ticket> CreateTicket(Ticket ticket);
        Task<Ticket> UpdateTicket(Ticket ticket);
        Task<bool> DeleteTicket(int id);
    }
}
