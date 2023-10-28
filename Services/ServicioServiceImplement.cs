using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDTelecomunicaciones.Data;
using JDTelecomunicaciones.Models;

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

        public Servicios GetServiceById(int idService){
            var myService = _context.DB_Servicios.Find(idService);
            return myService;
        }
    }
}