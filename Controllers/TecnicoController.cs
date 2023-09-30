using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using JDTelecomunicaciones.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JDTelecomunicaciones.Controllers
{
    [Route("[controller]")]
    public class TecnicoController : Controller
    {
        private readonly ILogger<TecnicoController> _logger;
        private readonly TicketServiceImplement _ticketService;
        public TecnicoController(ILogger<TecnicoController> logger,TicketServiceImplement ticketService)
        {
            _ticketService = ticketService;
            _logger = logger;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketService.GetTickets();
            return View("Index",tickets);
        }

        [HttpPost("BuscarTicket")]
        public async Task<IActionResult> BuscarTicket(string ticketId){
            
            int intTicketId = int.Parse(ticketId);

            var ticket = await _ticketService.GetTicketsById(intTicketId);
            return View("Index",ticket);
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error");
        }
    }
}