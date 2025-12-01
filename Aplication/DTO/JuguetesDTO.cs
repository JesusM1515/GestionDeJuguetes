using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class JuguetesDTO
    {
        [Required(ErrorMessage = "Ingresar el id del juguete es requerido")]
        public string ID_Juguete { get; set; } = string.Empty;

        [Required(ErrorMessage ="Ingresar el nombre del juguete es requerido")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingresar la categoria del juguete es requerido")]
        public string Categoria { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingresar el tipo de juguete es requerido")]
        public string Tipo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingresar el precio del juguete es requerido")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "Ingresar el stock del juguete es requerido")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Ingresar la edad recomendada del juguete es requerido")]
        public int Edad { get; set; }

        [Required(ErrorMessage = "Ingresar la descripcion del juguete es requerido")]
        public string Descripcion { get; set; } = string.Empty;
    }
}
