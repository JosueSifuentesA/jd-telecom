using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDTelecomunicaciones.Data;
using JDTelecomunicaciones.Models;
using Microsoft.EntityFrameworkCore;

namespace JDTelecomunicaciones.Services
{
    public class ReseñaServiceImplement
    {
        
        private readonly ApplicationDbContext _context;

        public ReseñaServiceImplement(ApplicationDbContext context){
            _context = context;
        }

        public async Task AddReview (Reseña reseña){
            await _context.DB_Reseñas.AddAsync(reseña);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Reseña>> GetAllReviews (){
            var reseñas = await _context.DB_Reseñas
            .Include(r=>r.Usuario)
                .ThenInclude(u => u.persona)
            .OrderBy(r=>r.Calificacion).ToListAsync();
            return reseñas;
        }
    
    }
}