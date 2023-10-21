using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JDTelecomunicaciones.Models
{
    [Table("planes")]
    public class Planes
    {
        [Column("id_plan")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id {get;set;}
        public decimal precio_plan {get;set;}
        public string nombre_plan {get;set;}
        public string descripcion_plan {get;set;}
        public int velocidad_plan {get;set;}
    }
}