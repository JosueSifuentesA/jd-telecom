using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JDTelecomunicaciones.Models
{
    [Table("ticket")]
    public class Tickets
    {
        [Column("id_ticket")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id_ticket {get;set;}
        public string tipoProblematica_ticket {get;set;}
        public string? descripcion_ticket {get;set;}
        public string status_ticket {get;set;}

        [Column(TypeName = "timestamp with time zone")]
        public DateTime fecha_ticket {get;set;}
         
        [Column("id_cliente")]
        public Usuario usuario {get;set;}

        [Column("id_tecnico")]
        public Usuario? tecnicoDesignado {get;set;}
    
    }
}