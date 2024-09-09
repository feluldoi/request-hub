using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RequestHub.Server.Data;
using RequestHub.Server.ServicesServer.TicketServiceServer;
using RequestHub.Shared;
using System.Security.Claims;

namespace RequestHub.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly ILogger<TicketController> _logger;
        private readonly ITicketServiceServer _ticketServiceServer;

        public TicketController(DataContext context, ILogger<TicketController> logger, ITicketServiceServer ticketServiceServer)
        {
            _context = context;
            _logger = logger;
            _ticketServiceServer = ticketServiceServer;
        }

        [HttpGet("tickets")]
        public async Task<ActionResult<List<Ticket>>> GetTickets()
        {
            try
            {
                _logger.LogInformation("GetTickets endpoint called");
                var allTickets = await _ticketServiceServer.GetTicketsAsync();
                _logger.LogInformation($"Retrieved {allTickets.Count} tickets from service");
                return Ok(allTickets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get all tickets failed");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetSingleTicket(int id)
        {
            try
            {
                _logger.LogInformation("Get SingleTicket endpoint called");
                var singleTicket = await _ticketServiceServer.GetSingleTicketAsync(id);
                _logger.LogInformation($"Retrieved {singleTicket.Id} ticket from service");
                return Ok(singleTicket);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get all tickets failed");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("departments")]
        public async Task<ActionResult<Department>> GetDepartments()
        {
            try
            {
                _logger.LogInformation("Get departments endpoint called");
                var departments = await _ticketServiceServer.GetDepartmentsAsync();
                _logger.LogInformation($"Retrieved {departments.Count} total departments ");
                return Ok(departments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get departments failed");
                return StatusCode(500, "Internal server error");
            }

        }


        [HttpGet("sitelocations")]
        public async Task<ActionResult<List<SiteLocation>>> GetSiteLocations()
        {

            try
            {
                var siteLocations = await _ticketServiceServer.GetSiteLocationsAsync();
                return Ok(siteLocations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");//this is a server side error
            }
        }

        [HttpGet("users")]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            try
            {
                var users = await _ticketServiceServer.GetUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get users failed");
                return StatusCode(500, "Internal server error");
            }



        }



        [HttpPost]
        public async Task<ActionResult<Ticket>> CreateTicket(Ticket ticket)
        {
            try
            {
                var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

                //_logger.LogInformation("Creating Ticket...");

                var createdTicket = await _ticketServiceServer.CreateTicketAsync(ticket, userId);
                return StatusCode(201, createdTicket);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a ticket");
                return StatusCode(500, "An unexpected error occurred while creating the ticket. Please try again later.");
            }

        }




        [HttpPut("{id}")]
        public async Task<ActionResult<List<Ticket>>> UpdateTicket(Ticket ticket, int id)
        {

            try
            {
                _logger.LogInformation("Controller: Updating Ticket...");

                var updatedTicket = await _ticketServiceServer.UpdateTicketAsync(ticket, id);
                return StatusCode(201, updatedTicket);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a ticket");
                return StatusCode(500, "An unexpected error occurred while updating the ticket. Please try again later.");
            }

        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteTicket(int id)
        {


            try
            {

                var deletedResult = await _ticketServiceServer.DeleteTicketAsync(id);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a ticket");
                return false;
            }









            //var dbTicket = await _context.Tickets
            //    .Include(sh => sh.Department)
            //    .Include(sh => sh.User)
            //    .Include(sh => sh.SiteLocation)
            //    //.Include(sh => sh.UploadFile)
            //    .FirstOrDefaultAsync(sh => sh.Id == id);
            //if (dbTicket == null)
            //    return NotFound("Sorry, but no ticket for you. :/");

            //_context.Tickets.Remove(dbTicket);
            //await _context.SaveChangesAsync();

            //return Ok(await GetDbTickets());
        }





        private async Task<List<Ticket>> GetDbTickets()
        {
            return await _context.Tickets
                .Include(sh => sh.Department)
                .Include(sh => sh.User)
                .Include(sh => sh.SiteLocation)
                //.Include(sh => sh.UploadFile)
                .ToListAsync();
        }
    }
}
