using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using JDTelecomunicaciones.Data;
using JDTelecomunicaciones.Models;
using JDTelecomunicaciones.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Sprache;

namespace JDTelecomunicaciones.Controllers
{
    [Route("[controller]")]
    public class ClienteController : Controller
    {

        private readonly TicketServiceImplement _ticketService;
        private readonly UsuarioServiceImplement _usuarioService;
        private readonly ReciboServiceImplement _reciboService;
        private readonly ApplicationDbContext _context;
        private readonly PlanesServiceImplement _planService;
        private readonly MercadoPagoServiceImplement _mercadoPagoService;
        private readonly ServicioServiceImplement _servicioService;

        public ClienteController(ServicioServiceImplement servicioService,PlanesServiceImplement planService,TicketServiceImplement ticketService,UsuarioServiceImplement usuarioService,ReciboServiceImplement reciboService,ApplicationDbContext context , MercadoPagoServiceImplement mercadoPagoService)
        {
            _ticketService = ticketService;
            _usuarioService = usuarioService;
            _reciboService = reciboService;
            _planService = planService;
            _mercadoPagoService = mercadoPagoService;
            _servicioService = servicioService;

            _context = context;
        }

        [Authorize(Roles="C")]
        [HttpGet("MisPlanes")]
        public async Task<IActionResult> MisPlanes(){

            var idUserClaim =  User.FindFirst("idUser")?.Value;
            int idUser = int.Parse(idUserClaim);

            var planes = _planService.GetAllPlans().Result;
            var usuario = await _usuarioService.FindUserById(idUser);

            Console.WriteLine($"El id del servicio es : {usuario}");
            var userService = _servicioService.GetServiceById(usuario.servicios.Id_servicios);

            ModeloConListas<Servicios, Planes> miModelo = new(userService,planes);
            return View("MisPlanes",miModelo);
        }

        [Authorize(Roles ="C")]
        [HttpGet("ServicioTecnico")]
        public async Task<IActionResult> ServicioTecnico(){
            var idUserClaim =  User.FindFirst("idUser")?.Value;
            int idUser = int.Parse(idUserClaim);

            var miUsuario = await _usuarioService.FindUserById(idUser);
            var tickets = await _ticketService.GetTicketsByUserId(idUser);

            var modeloConListas = new ModeloConListas<Usuario,Tickets>(miUsuario,tickets);

            return View("ServicioTecnico",modeloConListas);
        }
        [Authorize(Roles ="C")]
        [HttpPost("EnviarTicket")]
        public async Task<IActionResult> EnviarTicket(string tipoProblematica,string descripcion){
            DateTime fechaActual = DateTime.Today;
            string fechaActualS = fechaActual.ToString("dd/MM/yyyy");
            var idUserClaim = User.FindFirst("idUser")?.Value;
            int idUser = int.Parse(idUserClaim);
            var miUsuario = await _usuarioService.FindUserById(idUser);

            var ticket = new Tickets{ tipoProblematica_ticket = tipoProblematica,descripcion_ticket = descripcion,status_ticket = "PENDIENTE",usuario =miUsuario ,fecha_ticket = fechaActualS};
            _ticketService.AddTickets(ticket);
            return RedirectToAction("ServicioTecnico");
        }

        [Authorize(Roles ="C")]
        [HttpGet("RecibosPagados")]
        public async Task<IActionResult> RecibosPagados()
        {
            var idUserClaim = User.FindFirst("idUser").Value;
            if(idUserClaim != null){
                int idUser = int.Parse(idUserClaim);
 
                var recibosPagados = await _reciboService.GetAllCompletedVouchers(idUser);
                var recibosPendientes = await _reciboService.GetAllPendingVouchers(idUser);

                var recibos = new DobleLista<Recibos,Recibos>(recibosPagados,recibosPendientes);

                return View("RecibosPagados",recibos);

            }else{
                return View("Error");
            }
        }

        [Authorize(Roles ="C")]
        [Route("/Cliente/DetalleRecibo")]
        public IActionResult VerRecibos(int idRecibo){

            var recibo = _reciboService.GetVoucherById(idRecibo).Result;
            if(recibo == null){
                Console.WriteLine("No se encontro ningun recibo");
                return Error();
            }
            return View("DetalleRecibo",recibo);


        }

        [Authorize(Roles ="C")]
        [HttpPost("SolicitarPlan")]
        public async Task<IActionResult> SolicitarPlan([FromQuery] string planId){

            try{
                Console.WriteLine($"El planId seleccionado es : {planId}");
                int iPlanId = int.Parse(planId);
                var idUserClaim =  User.FindFirst("idUser")?.Value;
                int idUser = int.Parse(idUserClaim);

                var miPlan = _planService.GetPlanById(iPlanId).Result;

                if(miPlan != null){
                    var myService = new Servicios(){
                        FechaActivacion_Servicio= DateTime.Today.ToString("d/MM/yyyy"),
                        PeriodoFacturacion_Servicio=DateTime.Today.AddMonths(1).ToString("d/MM/yyyy"),
                        Estado_Servicio='A',
                        Plan_Servicio= miPlan

                    };
                    var myNuevoUsuario = _usuarioService.FindUserById(idUser).Result;
                    
                    if(myNuevoUsuario != null){
                        try{
                            _servicioService.AddService(myService);
                            myNuevoUsuario.servicios = myService;

                            await _usuarioService.EditUser(idUser,myNuevoUsuario);
                        }catch(Exception e){
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                return Ok();
            }catch(Exception e){
                Console.WriteLine(e.Message);
                return BadRequest(e);
            }
        }


        [Authorize(Roles ="C")]
        [HttpPost]
        public async Task<IActionResult> RecibosPagadosPorMes(string mes)
        {
            var idUserClaim = User.FindFirst("idUser").Value;
            if(idUserClaim != null){
                int idUser = int.Parse(idUserClaim);
                Console.WriteLine(mes);
                var recibosPagados = await _context.DB_Recibos.Include(r=>r.usuario).Where(recibos=>recibos.mes_recibo == mes && recibos.usuario.id_usuario==idUser && recibos.estado_recibo=="PAGADO").ToListAsync();
                var recibosPendientes = await _reciboService.GetAllPendingVouchers(idUser);

                if(mes == "Todos"){
                    recibosPagados = await _context.DB_Recibos.Include(r=>r.usuario).Where(recibos=>recibos.usuario.id_usuario==idUser && recibos.estado_recibo=="PAGADO").ToListAsync();
                }

                var recibos = new DobleLista<Recibos,Recibos>(recibosPagados,recibosPendientes);
                return View("RecibosPagados",recibos);
            }
            Console.WriteLine("No se encontro un usuario");
            return View("Error");
        }

        [Authorize(Roles ="C")]
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var idUserClaim = User.FindFirst("idUser").Value;
            if(idUserClaim != null){
                int idUser = int.Parse(idUserClaim);
                var recibosPendientes = await _reciboService.GetAllPendingVouchers(idUser);
                var user = await _usuarioService.FindUserById(idUser);
                ViewBag.email = user.correo_usuario; 

                DateTime now = DateTime.Now;
                DateTime nextMonth = now.AddMonths(1);
                DateTime firstDayOfNextMonth = new DateTime(nextMonth.Year, nextMonth.Month, 1);

                TimeSpan timeUntilNextMonth = firstDayOfNextMonth - now;
                int daysUntilNextMonth = (int)Math.Floor(timeUntilNextMonth.TotalDays);;

                ViewBag.nextVoucherTime = daysUntilNextMonth;

                return View("Index",recibosPendientes);

            }else{
                return View("Error");
            }
        }

        [Authorize(Roles ="C")]
        [HttpGet("/PagoExitoso")]
        public IActionResult PagoExitoso([FromQuery] string statusPago, [FromQuery] string statusMsg,[FromQuery] string data){

            dynamic paymentObject = Newtonsoft.Json.JsonConvert.DeserializeObject(data);
            Console.WriteLine($"{paymentObject.paymentId} - {paymentObject.paymentDate} - {paymentObject.paymentAmount} - {paymentObject.paymentCardLastFour}");
            ViewBag.StatusPago = statusPago;
            ViewBag.StatusMsg = statusMsg;

            ViewBag.PaymentId = paymentObject.paymentId;
            ViewBag.PaymentDate = paymentObject.paymentDate;
            ViewBag.PaymentAmount = paymentObject.paymentAmount;

            return View("PagoExitoso");
        }

        [Authorize(Roles ="C")]
        [HttpGet("/PagoFallido")]
        public IActionResult PagoFallido([FromQuery] string statusPago, [FromQuery] string statusMsg,[FromQuery] string data){

            
            dynamic paymentObject = Newtonsoft.Json.JsonConvert.DeserializeObject(data);
            Console.WriteLine(paymentObject.paymentAmount);
            Console.WriteLine(data);
            ViewBag.StatusMsg = statusMsg;

            return View("PagoFallido");
        }

        [Authorize(Roles ="C")]
        [HttpGet("/PagoEnProceso")]
        public IActionResult PagoEnProceso(){
            return View("PagoEnProceso");
        }


        [Authorize(Roles ="C")] 
        [HttpPost("/process_payment")]
        public async Task<IActionResult> process_payment(){
            using (var reader = new StreamReader(Request.Body))
            {
                var requestBody = await reader.ReadToEndAsync();

                dynamic requestData = Newtonsoft.Json.JsonConvert.DeserializeObject(requestBody);

                var token = requestData.token;
                var issuerId = requestData.issuer_id;
                var paymentMethodId = requestData.payment_method_id;
                var transactionAmount = requestData.transaction_amount;
                var installments = requestData.installments;
                var email = requestData.email;
                var type = requestData.type;
                var number = requestData.number;
                string descripcion = "TESTPRUEBADESCRIPCION";

                Console.WriteLine(requestBody);
                var paymentStatus = await _mercadoPagoService.CrearPago(requestData, descripcion);
                Console.WriteLine(paymentStatus);

                return Json(paymentStatus);
            }
             
        }

        [Authorize(Roles ="C")] //Añadido recientemente
        [HttpPost("/pagar_recibos")]
        public async Task<IActionResult> pagar_recibos(dynamic transaccion , int[] recibos){
            using (var reader = new StreamReader(Request.Body))
            {
                var requestBody = await reader.ReadToEndAsync();
                Console.WriteLine(requestBody);
                dynamic requestData = Newtonsoft.Json.JsonConvert.DeserializeObject(requestBody);
                JArray voucherIdArray = (JArray)requestData["voucherId"];

                foreach(var item in voucherIdArray){ 
                    await _reciboService.PayVoucher(Convert.ToInt32(item));    
                }

                Console.WriteLine($"{voucherIdArray} - {requestData.data.transactionAmount}");
                return Ok();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }

    internal class ModeloConListas
    {
    }
}