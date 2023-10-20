using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using JDTelecomunicaciones.Models;
using Microsoft.Extensions.DependencyInjection;

namespace JDTelecomunicaciones.Services
{
    public class ReciboHostedService : IHostedService
    {

        private System.Threading.Timer? _timer;

        private IServiceProvider _serviceProvider;

        public ReciboHostedService(IServiceProvider serviceProvider)
        {
            //cls_serviceFactory = serviceFactory;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
    {
        DateTime now = DateTime.Now;
        DateTime nextMonth = now.AddMonths(1);
        DateTime firstDayOfNextMonth = new DateTime(nextMonth.Year, nextMonth.Month, 1);
        TimeSpan timeUntilNextMonth = firstDayOfNextMonth - now;


        Console.WriteLine($"FechaActual {now} - PROXIMO MES : {nextMonth} - Primer Dia del siguiente mes : {firstDayOfNextMonth} - Tiempo hasta el proximo mes : {timeUntilNextMonth}");

        _timer = new System.Threading.Timer(async state=>{await GenerarRecibo(state);}, null, timeUntilNextMonth, TimeSpan.FromDays(30));

        //_timer = new System.Threading.Timer(async state=>{await GenerarRecibo(state);}, null, TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(30));

        return Task.CompletedTask;
    }



        public void message(object state){
            Console.WriteLine("MESSAGE");
        }

        public async  Task GenerarRecibo(object state){
            Console.WriteLine("Se ejecuto generar recibo");
            DateTime fechaActual = DateTime.Today;
            //DateTime fechaActual = new DateTime(2023, 11, 1);
            DateTime fechaGeneracionRecibo = new DateTime(fechaActual.Year, fechaActual.Month, 1);
            DateTime fechaVencimiento = fechaGeneracionRecibo.AddMonths(1).AddDays(-1);
            string nombreMes = fechaActual.ToString("MMMM");

            Console.WriteLine("SE EJECUTÓ GENERAR RECIBO - Fecha actual: " + fechaActual + ", Fecha de generación de recibo: " + fechaGeneracionRecibo);

            if (fechaActual == fechaGeneracionRecibo)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _reciboService = scope.ServiceProvider.GetRequiredService<ReciboServiceImplement>();
                    var _usuarioService = scope.ServiceProvider.GetRequiredService<UsuarioServiceImplement>();

                    try
                    {
                        var recibos = await _reciboService.GetAllMonthlyUserVouchers(1, "");
                        var usuarios = await _usuarioService.GetUsers();

                        foreach (var usuario in usuarios)
                        {
                            Console.WriteLine("Se generó un recibo para " + usuario.nombre_usuario + " (ID: " + usuario.id_usuario + ")");
                            
                            if (usuario != null)
                            {
                                var recibo = new Recibos
                                {
                                    plan_recibo = "JD_BASICO",
                                    mes_recibo = nombreMes,
                                    fecha_vencimiento = fechaVencimiento.ToString("d/MM/yyyy"),
                                    monto_recibo = 30.00m,
                                    estado_recibo = "PENDIENTE",
                                    usuario = usuario
                                };

                                await _reciboService.AddVoucher(recibo);
                                Console.WriteLine("SE AÑADIÓ EL RECIBO AL USUARIO: " + usuario.nombre_usuario + " (ID: " + usuario.id_usuario + ")");
                            }
                        }

               
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("ERROR: " + e.Message);
                    }
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    
        public void Dispose()
        {
            _timer?.Dispose();
        }

    }
}
