using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDTelecomunicaciones.Data;
using JDTelecomunicaciones.Models;
using Microsoft.EntityFrameworkCore;

namespace JDTelecomunicaciones.Services
{
    public class PlanesServiceImplement: IPlanesService
    {

        private readonly ApplicationDbContext _context;

        public PlanesServiceImplement(ApplicationDbContext context){
            _context = context;
        }

        public async Task<List<Planes>> GetAllPlans(){
            try
            {
                var planes = await _context.DB_Planes.ToListAsync();
                return planes;
            }
            catch (Exception e)
            {   
                Console.WriteLine(e.Message);
                return new List<Planes>() ;
                throw;
            }
        }

        public async Task<Planes> GetPlanById(int planId){
            try
            {
                var planes = await _context.DB_Planes.FindAsync(planId);
                return planes;
            }
            catch (Exception e)
            {   
                Console.WriteLine(e.Message);
                return null ;
                throw;
            }
        }

        public async Task<List<Planes>> GetPlanesFiltrados(int min,int max){
            try
            {
                var preciosFiltrados = await _context.DB_Planes
                    .Where(p => p.precio_plan >= min && p.precio_plan <= max)
                    .ToListAsync();
                return preciosFiltrados;
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