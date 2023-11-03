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


namespace JDTelecomunicaciones.Controllers
{
    [Route("[controller]")]
    public class AdministracionController : Controller
    {
        private readonly ILogger<AdministracionController> _logger;
        private readonly UsuarioServiceImplement _usuarioService;
        private readonly PromocionServiceImplement _promocionService;
        private readonly ApplicationDbContext _context;

        public AdministracionController(PromocionServiceImplement promocionService,ApplicationDbContext context,ILogger<AdministracionController> logger,UsuarioServiceImplement usuarioService){
            _logger = logger;
            _usuarioService = usuarioService;
            _context = context;
            _promocionService=promocionService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index(){
            Console.WriteLine("INDEX ADMIN");
            var promociones = await _promocionService.GetAllPromotions();
            return View("Index",promociones);
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