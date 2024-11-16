using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using RequestHub.Server.Data;
using RequestHub.Shared;
using System.ComponentModel;

namespace RequestHub.Server.ServicesServer.TicketServiceServer
{
    public class TicketServiceServer : ITicketServiceServer
    {
        //Handes calls to the database


        private readonly DataContext _context;
        private readonly ILogger<TicketServiceServer> _logger;

        public TicketServiceServer(DataContext context, ILogger<TicketServiceServer> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Ticket>> GetTicketsAsync()
        {
            _logger.LogInformation("Fetching tickets from database");
            var tickets = await _context.Tickets
                .Include(sh => sh.Department)
                .Include(sh => sh.User)
                .Include(sh => sh.SiteLocation)
                //.Include(sh => sh.UploadFile)
                .ToListAsync();

            _logger.LogInformation($"Retrieved {tickets.Count} tickets");
            return tickets;
        }



        public async Task<Ticket> GetSingleTicketAsync(int id)
        {
            _logger.LogInformation("fetching single ticket from database");
            var ticket = await _context.Tickets
                .Include(h => h.Department)
                .Include(h => h.User)
                .Include(h => h.SiteLocation)
                //.Include(h => h.UploadFile)
                .FirstOrDefaultAsync(h => h.Id == id);

            _logger.LogInformation($"Retrieved {id} ticket");
            return ticket;

        }



        public async Task<List<Department>> GetDepartmentsAsync()
        {
            var departments = await _context.Departments.ToListAsync();
            return departments;

        }

        public async Task<List<SiteLocation>> GetSiteLocationsAsync()
        {
            var siteLocations = await _context.SiteLocations.ToListAsync();
            return siteLocations;
        }


        public async Task<List<User>> GetUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }







        public async Task<Ticket> CreateTicketAsync(Ticket ticket, int userId)
        {

            ticket.UserId = userId;
            ticket.Department = null;
            ticket.User = null;
            ticket.SiteLocation = null;
            //ticket.UploadFile = null;

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return ticket;


        }


  
        public async Task<Ticket> UpdateTicketAsync(Ticket ticket, int id)
        {
            
            var updatedTicket = await _context.Tickets
                .Include(sh => sh.Department)
                .Include(sh => sh.User)
                .Include(sh => sh.SiteLocation)
                //.Include(sh => sh.UploadFile)
                 .FirstOrDefaultAsync(sh => sh.Id == id);

            //add all columns that can be updated here
            updatedTicket.Description = ticket.Description;
            updatedTicket.IsValid = ticket.IsValid;
            updatedTicket.DepartmentId = ticket.DepartmentId;
            updatedTicket.UserId = ticket.UserId;
            updatedTicket.EquipmentName = ticket.EquipmentName;
            updatedTicket.SiteLocationId = ticket.SiteLocationId;
            updatedTicket.Comment = ticket.Comment;
            //updatedTicket.UploadFileId = ticket.UploadFileId;

            await _context.SaveChangesAsync();

            return ticket;
            //return Ok(await GetDbTickets());
        }




        public async Task<bool> DeleteTicketAsync(int id)
        {
            //Remove ticket 
            var dbTicket = await _context.Tickets
                .Include(sh => sh.Department)
                .Include(sh => sh.User)
                .Include(sh => sh.SiteLocation)
                //.Include(sh => sh.UploadFile)
                 .FirstOrDefaultAsync(sh => sh.Id == id);

            //Remove associated uploadfiles
            var uploadFiles = await _context.UploadFiles
                .Where(item => item.TicketId == id).ToListAsync();

            _context.UploadFiles.RemoveRange(uploadFiles);
            _context.Tickets.Remove(dbTicket);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
