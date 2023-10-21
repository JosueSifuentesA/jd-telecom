using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDTelecomunicaciones.Data;
using JDTelecomunicaciones.Models;

namespace JDTelecomunicaciones.Services
{
    public class PlanesServiceImplement: IPlanesService
    {

        private readonly ApplicationDbContext _context;

        public PlanesServiceImplement(ApplicationDbContext context){
            _context = context;
        }

        public List<Planes> GetAllPlans(){
            try
            {
                var planes = _context.DB_Planes.ToList();
                return planes;
            }
            catch (Exception e)
            {   
                Console.WriteLine(e.Message);
                return new List<Planes>() ;
                throw;
            }
        }
    }
}