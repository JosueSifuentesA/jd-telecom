using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace JDTelecomunicaciones.Models
{
    [Table("servicios")]
    public class Servicios
    {

        [Column("id_servicios")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id_servicios {get;set;}

        public string FechaActivacion_Servicio {get;set;}
        public string PeriodoFacturacion_Servicio {get;set;}

        public char Estado_Servicio {get;set;}

        public Planes Plan_Servicio {get;set;}

    }
}