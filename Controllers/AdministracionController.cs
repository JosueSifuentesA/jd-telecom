using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using JDTelecomunicaciones.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JDTelecomunicaciones.Models;
using JDTelecomunicaciones.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;

namespace JDTelecomunicaciones.Controllers
{
    [Route("[controller]")]
    public class AdministracionController : Controller
    {
        private readonly ILogger<AdministracionController> _logger;
        private readonly UsuarioServiceImplement _usuarioService;
        private readonly PromocionServiceImplement _promocionService;
        private readonly ApplicationDbContext _context;
        private readonly ReciboServiceImplement _recibosService;
        private readonly ReseñaServiceImplement _reseñaService;

        public AdministracionController(ReseñaServiceImplement reseñaService,ReciboServiceImplement recibosService,PromocionServiceImplement promocionService,ApplicationDbContext context,ILogger<AdministracionController> logger,UsuarioServiceImplement usuarioService){
            _logger = logger;
            _usuarioService = usuarioService;
            _context = context;
            _promocionService=promocionService;
            _reseñaService=reseñaService;
            _recibosService= recibosService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index(){
            Console.WriteLine("INDEX ADMIN");
            var promociones = await _promocionService.GetAllPromotions();
            return View("Index",promociones);
        }

        [HttpGet("GetAllMontlyCompletedVouchers")]
        public async Task<IActionResult> GetAllMontlyCompletedVouchers(){
            var recibos = await _recibosService.GetAllMontlyCompletedVouchers();
            return Json(recibos);
        }


        //[HttpGet("GetYearlyCompletedTickets")]
        /*public async Task<IActionResult> GetYearlyCompletedTickets()
        {
            var yearlyTickets = await _context.DB_Tickets
                .Where(t => t.status_ticket == "REALIZADO" && t.fecha_ticket != null)
                .GroupBy(t => DateTime.Parse(t.fecha_ticket).Year)
                .Select(group => new { Year = group.Key, TotalTickets = group.Count() })
                .ToListAsync();

            return Json(yearlyTickets);
        }
        */
        
        /*[HttpGet("GetYearlyCompletedTickets")]
        public async Task<IActionResult> GetYearlyCompletedTickets()
        {
            var yearlyTickets = await _context.DB_Tickets
                .Where(t => t.status_ticket == "REALIZADO" && t.fecha_ticket != null)
                .Select(t => new
                {
                    Year = DateTime.ParseExact(t.fecha_ticket, "yyyy/MM/dd", CultureInfo.InvariantCulture).Year,
                    Ticket = t
                })
                .GroupBy(t => t.Year)
                .Select(group => new { Year = group.Key, TotalTickets = group.Count() })
                .ToListAsync();

            return Json(yearlyTickets);
        }
        */

        [HttpGet("GetYearlyCompletedTickets")]
        public async Task<IActionResult> GetYearlyCompletedTickets()
        {
            var yearlyTickets = await _context.DB_Tickets
                .Where(t => t.status_ticket == "REALIZADO" && t.fecha_ticket != null)
                .GroupBy(t => t.fecha_ticket.Year)
                .Select(group => new { Year = group.Key, TotalTickets = group.Count() })
                .ToListAsync();

            return Json(yearlyTickets);
        }


        [HttpGet("GetMonthlyCompletedTickets")]
        public async Task<IActionResult> GetMonthlyCompletedTickets()
        {
            var monthlyTickets = await _context.DB_Tickets
                .Where(t => t.status_ticket == "REALIZADO" && t.fecha_ticket != null)
                .GroupBy(t => new { Year = t.fecha_ticket.Year, Month = t.fecha_ticket.Month })
                .Select(group => new
                {
                    Year = group.Key.Year,
                    Month = group.Key.Month,
                    TotalTickets = group.Count()
                })
                .ToListAsync();

            return Json(monthlyTickets);
        }



        [Authorize(Roles ="A")]
        [HttpGet("GetAllReviewsByPlan")]
        public async Task<IActionResult> GetAllReviewsByPlan(){
            var reseñas = await _context.DB_Planes
                .Select(plan => new
                {
                    PlanId = plan.id,
                    NombrePlan = plan.nombre_plan,
                    ReseñasPositivas = _context.DB_Reseñas
                        .Count(reseña => reseña.PlanReseña.id == plan.id && reseña.Calificacion >= 3 && reseña.Calificacion <= 5)
                })
                .ToListAsync();
            return Json(reseñas);
        }
        

        [Authorize(Roles ="A")]
        [HttpGet("InformeDesempeño")]
        public async Task<IActionResult> InformeDesempeño(){
            return View("InformeDesempeño");
        }

        [Authorize(Roles ="A")]
        [HttpGet("GetAllComplaints")]
        public async Task<IActionResult> GetAllComplaints(){
           var reclamacionesPorMes = await _context.DB_Reclamaciones
                .GroupBy(reclamacion => new { Mes = reclamacion.FechaPublicacion.Month, Año = reclamacion.FechaPublicacion.Year })
                .Select(grupo => new
                {
                    Mes = grupo.Key.Mes,
                    Año = grupo.Key.Año,
                    CantidadReclamaciones = grupo.Count()
                })
                .ToListAsync();
            return Json(reclamacionesPorMes);
        }

        [HttpGet("Ganancias")]
        public IActionResult Ganancias(){
            //var recibos = await _recibosService.GetAllMontlyCompletedVouchers();
            return View("Ganancias");
        }
        
        [HttpGet("ListaClientes")]
        public async Task<IActionResult> ListaClientes(){
            var users = await _usuarioService.GetClientUsers();
            return View("ListaClientes",users);
        }

        [HttpGet("Promociones")]
        public IActionResult Promociones()
        {
            return View("Promociones");
        }

        [HttpGet("ObtenerPromociones")]
        public async Task<string> ObtenerPromociones(){
            var myList = await _promocionService.GetAllPromotions();
            string jsonString = JsonSerializer.Serialize(myList);
            return  jsonString;
        }
        
        [HttpPost("RegistrarPromocionPost")]
        public async Task<IActionResult> RegistrarPromocionPost(string nombrePromocion, string efectoPromocion, UploadImgModel fileImage)
        {
            //var efectos = _efectosService.ListarEfectos();
            //Console.WriteLine(nombrePromocion + " " + efectoPromocion + " " + fileImage.imgSubida);
            using (var ms = new MemoryStream())
            {
                if (fileImage != null && fileImage.imgSubida != null)
                {
                    if(ModelState.IsValid){
                        await fileImage.imgSubida.CopyToAsync(ms);
                        Console.WriteLine(ms.ToString());
                        var imgSubidaByte = ms.ToArray();
                        Console.WriteLine(nombrePromocion + " "+efectoPromocion + " " + imgSubidaByte.Length);
                        
                        var promocion = new Promocion
                        {
                            nombre_promocion=nombrePromocion,
                            efecto_promocion=efectoPromocion,
                            imgSubidaByte=imgSubidaByte,
                        };

                        Console.WriteLine(promocion.nombre_promocion+ "-" + promocion.efecto_promocion +"-"+ promocion.imgSubidaByte);

                        await _promocionService.AddPromotion(nombrePromocion,efectoPromocion,imgSubidaByte);
                    }
                }
                else
                {
                    Console.WriteLine("No se proporcionó ninguna imagen");
                }
            }

            return RedirectToAction("Promociones","Administracion");
        }

        [HttpPost("AsignarPromocion")]
        public async Task<IActionResult> AsignarPromocion(string inputIds,string efectoId){

            Console.WriteLine(efectoId);
            int[] ids = JsonSerializer.Deserialize<int[]>(inputIds);
            var promocion = _promocionService.GetPromotionById(int.Parse(efectoId)).Result;

            foreach (var userId in ids)
            {
                var usuario = await _context.DB_Usuarios.FirstOrDefaultAsync(u => u.id_usuario == userId);

                if (usuario != null)
                {
                    usuario.promociones = promocion; // Asigna el ID de la promoción al usuario
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}