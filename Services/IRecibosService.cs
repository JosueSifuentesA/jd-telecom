using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDTelecomunicaciones.Models;

namespace JDTelecomunicaciones.Services
{
    public interface IRecibosService
    {
        public Task<List<Recibos>> GetAllUserVouchers(int userId);
        public Task<List<Recibos>> GetAllMonthlyUserVouchers(int userId,string mes);
        public Task AddVoucher(Recibos recibo);
        public Task DeleteVoucher(int id);
        public Task<Recibos> GetVoucherById(int voucherId);

        //public void GenerateVoucherPDF(string pdfName, Usuario usuario,Servicios servicio,Planes plan);
        public byte[] GeneratePDFContent(Usuario usuario, Servicios servicio, Planes plan);
    }
}