using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JDTelecomunicaciones.Data;
using JDTelecomunicaciones.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace JDTelecomunicaciones.Services
{
    public class ReciboServiceImplement : IRecibosService
    {
        
        private Timer? _timer;
        private System.Threading.Timer? _timer2;
        private readonly ApplicationDbContext _context;
        public ReciboServiceImplement(ApplicationDbContext context){
            _context = context;
        }

        public async Task AddVoucher(Recibos recibo)
        {
            try{
                if(recibo != null){
                    await _context.DB_Recibos.AddAsync(recibo);
                    await _context.SaveChangesAsync();
                }
            }catch(Exception e){
                Console.WriteLine("ERROR : " + e.Message);
            }
            
        }

        public Task DeleteVoucher(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Recibos>> GetAllMonthlyUserVouchers(int userId, string mes)
        {
            var recibos = await _context.DB_Recibos.Include(r=>r.usuario).ToListAsync();
            return recibos;
        }

        public async Task<List<Recibos>> GetAllUserVouchers(int userId)
        {
            var recibos = await _context.DB_Recibos.Include(r=>r.usuario).Where(r=>r.usuario.id_usuario == userId).ToListAsync();
            return recibos;
        }

        public async Task<List<Recibos>> GetAllCompletedVouchers(int userId)
        {
            var recibos = await _context.DB_Recibos.Include(r=>r.usuario).Where(r=>r.usuario.id_usuario == userId && r.estado_recibo == "PAGADO").ToListAsync();
            return recibos;
        }

        public async Task<List<Recibos>> GetAllPendingVouchers(int userId)
        {
            var recibos = await _context.DB_Recibos.Include(r=>r.usuario).Where(r=>r.usuario.id_usuario == userId && r.estado_recibo == "PENDIENTE").ToListAsync();
            return recibos;
        }

        public Recibos GetVoucherById(int voucherId)
        {
            throw new NotImplementedException();
        }
    }
}