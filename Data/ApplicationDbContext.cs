using Microsoft.EntityFrameworkCore;
using JDTelecomunicaciones.Models;

namespace JDTelecomunicaciones.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<MensajeContacto> DB_ContactMessages { get; set; }
        public DbSet<Usuario> DB_Usuarios {get;set;}
        public DbSet<Tickets> DB_Tickets {get;set;}
        public DbSet<Servicios> DB_Servicios {get;set;}
        public DbSet<Persona> DB_Personas {get;set;}
        public DbSet<Promocion> DB_Promociones {get;set;}
        public DbSet<Recibos> DB_Recibos {get;set;}
        public DbSet<Planes> DB_Planes {get;set;}
    }
}