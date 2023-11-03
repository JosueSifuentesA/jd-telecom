using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDTelecomunicaciones.Data;
using JDTelecomunicaciones.Models;
using Microsoft.EntityFrameworkCore;

namespace JDTelecomunicaciones.Services
{
    public class ServicioServiceImplement
    {
        private readonly ApplicationDbContext _context ;
        public ServicioServiceImplement(ApplicationDbContext context){
            _context = context;
        }
        public void AddService(Servicios myService){
            _context.DB_Servicios.Add(myService);
            _context.SaveChanges();
        }

        public async Task<Servicios> GetServiceById(int idService){
            var myService = await _context.DB_Servicios.Include(s=>s.Plan_Servicio).FirstOrDefaultAsync(s => s.Id_servicios == idService);
            return myService;
        }

        public async Task UpdatePlanService(int idService,Planes plan){
            var myService = _context.DB_Servicios.Find(idService);
            myService.Plan_Servicio = plan;
            await _context.SaveChangesAsync();
        }

    }
}