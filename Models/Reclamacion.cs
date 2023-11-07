using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JDTelecomunicaciones.Models
{
    [Table("Reclamaciones")]
    public class Reclamacion
    {
        [Column("id_reclamacion")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Contenido { get; set; }
        
        public string TipoReclamacion { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}