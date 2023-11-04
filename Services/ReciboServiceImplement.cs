using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JDTelecomunicaciones.Data;
using JDTelecomunicaciones.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;


namespace JDTelecomunicaciones.Services
{
    public class ReciboServiceImplement : IRecibosService
    {
        
        //private Timer? _timer;
        //private System.Threading.Timer? _timer2;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ReciboServiceImplement(IWebHostEnvironment hostingEnvironment,ApplicationDbContext context){
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task PayVoucher(int id){
            try{
                var recibo = await _context.DB_Recibos.FindAsync(id);
                recibo.estado_recibo = "PAGADO";
                await _context.SaveChangesAsync();
            }catch(Exception e){
                Console.WriteLine($"{e.Message}- {e.HResult}");
            }
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

        public async Task<Recibos> GetVoucherById(int voucherId)
        {
            try{
                var recibos =  await _context.DB_Recibos.FindAsync(voucherId);
                return recibos;
            }catch(Exception e){
                var recibo = new Recibos(){
                    idRecibo = 9999,
                    plan_recibo = "",
                    mes_recibo = "",
                    fecha_vencimiento = "",
                    monto_recibo = 0,
                };
                Console.WriteLine(e.Message);
                return recibo;
            }
        }

        public byte[] GeneratePDFContent(Usuario usuario, Servicios servicio, Planes plan){
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
                doc.Open();
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font = new Font(bf, 12);
                Persona persona = usuario.persona;

                string imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "img/logo-jd.png");
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imagePath);
                logo.SetAbsolutePosition(doc.Right - 100, doc.Top - 50);
                logo.ScaleAbsolute(120, 60); 
                doc.Add(logo);

                doc.Add(new Paragraph("Recibo de Servicio", new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD)));
                doc.Add(Chunk.NEWLINE);

                doc.Add(new Paragraph("Nombre: " + persona.nombrePersona, font));
                doc.Add(new Paragraph("Apellido Paterno: " + persona.apPatPersona, font));
                doc.Add(new Paragraph("Apellido Materno: " + persona.apMatPersona, font));
                doc.Add(new Paragraph("DNI: " + persona.dniPersona, font));
                doc.Add(Chunk.NEWLINE);

                doc.Add(new Paragraph("Fecha de Activación del Servicio: " + servicio.FechaActivacion_Servicio, font));
                doc.Add(new Paragraph("Período de Facturación: " + servicio.PeriodoFacturacion_Servicio, font));
                doc.Add(new Paragraph("Estado del Servicio: " + servicio.Estado_Servicio, font));
                doc.Add(Chunk.NEWLINE);

                doc.Add(new Paragraph("Detalles del Plan de Servicio:", new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD)));
                doc.Add(new Paragraph("Nombre del Plan: " + plan.nombre_plan, font));
                doc.Add(new Paragraph("Precio del Plan: " + plan.precio_plan.ToString("C"), font));
                doc.Add(new Paragraph("Velocidad del Plan: " + plan.velocidad_plan + " Mbps", font));

                doc.Close();
                writer.Close();

                return memoryStream.ToArray();
            }
        }



    }
}