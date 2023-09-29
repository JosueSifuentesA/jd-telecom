using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using JDTelecomunicaciones.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JDTelecomunicaciones.Controllers
{
    [Route("[controller]")]
    public class AdministracionController : Controller
    {
        private readonly ILogger<AdministracionController> _logger;
        private readonly UsuarioServiceImplement _usuarioService;

        public AdministracionController(ILogger<AdministracionController> logger,UsuarioServiceImplement usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            Console.WriteLine("INDEX ADMIN");
            return View("Index");
        }

        [HttpGet("ListaClientes")]
        public async Task<IActionResult> ListaClientes()
        {
            var users = await _usuarioService.GetUsers();
            return View("ListaClientes",users);
        }

        [HttpGet("Promociones")]
        public IActionResult Promociones()
        {
            return View("Promociones");
        }

        [HttpPost("AsignarPromocion")]
        public IActionResult AsignarPromocion(string inputIds){
            //Console.WriteLine(inputIds.Length);
            //return View("Index");
            // Deserializa la cadena JSON en un arreglo de enteros
            int[] ids = JsonSerializer.Deserialize<int[]>(inputIds);
            for(int i = 0 ; i < ids.Length;i++){
                Console.WriteLine("Ids"+ids[i]);
            }
            // Tu código aquí para trabajar con el arreglo de IDs

            return View("Index");

        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}