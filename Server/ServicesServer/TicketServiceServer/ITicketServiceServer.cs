using RequestHub.Shared;

namespace RequestHub.Server.ServicesServer.TicketServiceServer
{
    public interface ITicketServiceServer
    {
        Task<List<Ticket>> GetTicketsAsync();
        Task<Ticket> GetSingleTicketAsync(int id);
        Task<List<Department>> GetDepartmentsAsync();
        Task<List<SiteLocation>> GetSiteLocationsAsync();
        Task<List<User>> GetUsersAsync();
        Task<Ticket> CreateTicketAsync(Ticket ticket, int userId);
        Task<Ticket> UpdateTicketAsync(Ticket ticket, int id);
        Task<bool> DeleteTicketAsync(int id);

    }
}
