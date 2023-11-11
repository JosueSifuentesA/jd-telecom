using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using JDTelecomunicaciones.Services;
using JDTelecomunicaciones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JDTelecomunicaciones.Controllers
{
    [Route("[controller]")]
    public class TecnicoController : Controller
    {
        private readonly ILogger<TecnicoController> _logger;
        private readonly TicketServiceImplement _ticketService;
         private readonly UsuarioServiceImplement _usuarioService;
        public TecnicoController(UsuarioServiceImplement usuarioService, ILogger<TecnicoController> logger,TicketServiceImplement ticketService)
        {
            _ticketService = ticketService;
            _logger = logger;
            _usuarioService = usuarioService;
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

        [HttpGet("BuscarTicketCompletado")]
        public async Task<IActionResult> BuscarTicketCompletado(string ticketId){
            
            int intTicketId = int.Parse(ticketId);

            var ticket = await _ticketService.GetTicketCompletedById(intTicketId);
            List<Tickets> tickets = new List<Tickets>();
            if(ticket != null){
                tickets.Add(ticket);
            }
            Console.WriteLine(ticket + " - " + tickets.Count());
            return View("TareasHechas",tickets);
            
        }



        [HttpGet("TareasHechas")]
        public async Task<IActionResult> TareasHechas(){

            var ticketsTerminados = await _ticketService.GetSuccesfulTickets();

            return View("TareasHechas",ticketsTerminados);
        }

        [HttpGet("InformacionTicket")]
        public async Task<IActionResult> InformacionTicket(int idTicket){

            var ticket = await _ticketService.GetTicketById(idTicket);
            if(ticket.status_ticket != "REALIZADO"){
                ticket.status_ticket = "VISTO";
            }
            await _ticketService.EditTicket(idTicket,ticket);
            Console.WriteLine("SE ABRIO TICKETS" + ticket.status_ticket);
            return View("InformacionTicket",ticket);
        }

        [HttpGet("FiltrarTickets")]
        public async Task<IActionResult> FiltrarTickets(string tipoTicket){

            if(tipoTicket == "TODO") return RedirectToAction("Index");

            var tickets = await _ticketService.GetTicketByType(tipoTicket);
            
            //Console.WriteLine("SE ABRIO TICKETS" + ticket.status_ticket);
            return View("Index",tickets);
        }

        //[Authorize(Roles ="A")]
        [HttpGet("PerfilTecnico")]
        public async Task<IActionResult> PerfilTecnico(){
            var idUserClaim =  User.FindFirst("idUser")?.Value;
            int idUser = int.Parse(idUserClaim);
            Console.WriteLine($"ID USUARIO:{idUser}");
            var ticketsResueltos = await _ticketService.GetTicketCompletedByUserId(idUser);
            return View("PerfilTecnico",ticketsResueltos);
        }


        [HttpGet("MarcarTarea")]
        public async Task<IActionResult> MarcarTarea(int idTicket){
            
            var idUserClaim =  User.FindFirst("idUser")?.Value;
            int idUser = int.Parse(idUserClaim);
            var usuario = _usuarioService.FindUserById(idUser).Result;

            var ticket = await _ticketService.GetTicketById(idTicket);
            ticket.status_ticket ="REALIZADO" ;
            ticket.tecnicoDesignado = usuario;
            await _ticketService.EditTicket(idTicket,ticket);
            return RedirectToAction("TareasHechas");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error");
        }
    }
}