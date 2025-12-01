using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EJuguetes
    {
        [Key]
        public int Key_Juguetes { get; set; }
        public string ID_Juguete { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int Edad {  get; set; }
        public string Descripcion {  get; set; } = string.Empty;
    }
}
