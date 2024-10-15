using Microsoft.AspNetCore.Mvc;

namespace TicketManager.Model
{
    public class TicketRepository
    {
        private List<Ticket> _tickets = new List<Ticket>();

        public List<Ticket> GetAll() => _tickets;

        public Ticket GetById(int id) => _tickets.FirstOrDefault(t => t.TicketId == id);

        public void Add(Ticket ticket) => _tickets.Add(ticket);

        public void Update(Ticket updatedTicket)
        {
            var ticket = GetById(updatedTicket.TicketId);
            if (ticket != null)
            {
                ticket.Description = updatedTicket.Description;
                ticket.Status = updatedTicket.Status;
                ticket.Date = updatedTicket.Date;
            }
        }

        public void Delete(int id)
        {
            var ticket = GetById(id);
            if (ticket != null)
                _tickets.Remove(ticket);
        }
    }
    Controller(API Endpoints) Implement a controller to expose CRUD endpoints.

    csharp
    Copy code
    [ApiController]
    [Route("api/[controller]")]
public class TicketsController : ControllerBase
    {
        private readonly TicketRepository _repository = new TicketRepository();

        [HttpGet]
        public IActionResult GetTickets() => Ok(_repository.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetTicket(int id)
        {
            var ticket = _repository.GetById(id);
            if (ticket == null) return NotFound();
            return Ok(ticket);
        }

        [HttpPost]
        public IActionResult CreateTicket([FromBody] Ticket newTicket)
        {
            newTicket.TicketId = _repository.GetAll().Count + 1;
            newTicket.Date = DateTime.Now;
            _repository.Add(newTicket);
            return CreatedAtAction(nameof(GetTicket), new { id = newTicket.TicketId }, newTicket);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTicket(int id, [FromBody] Ticket updatedTicket)
        {
            if (id != updatedTicket.TicketId) return BadRequest();
            _repository.Update(updatedTicket);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTicket(int id)
        {
            _repository.Delete(id);
            return NoContent();
        }
    }
}
}
