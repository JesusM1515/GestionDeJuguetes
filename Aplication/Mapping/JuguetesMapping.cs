using Application.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class JuguetesMapping
    { 
        public EJuguetes MapDTOAdd(JuguetesDTO eJuguetesDTO)
        {
            EJuguetes mapeo = new EJuguetes
            {
                ID_Juguete = eJuguetesDTO.ID_Juguete,
                Nombre = eJuguetesDTO.Nombre,
                Categoria = eJuguetesDTO.Categoria,
                Tipo  = eJuguetesDTO.Tipo,
                Precio = eJuguetesDTO.Precio,
                Stock = eJuguetesDTO.Stock,
                Edad = eJuguetesDTO.Edad,
                Descripcion = eJuguetesDTO.Descripcion
            };
            return mapeo;
        }

        //Devolver juguete
        public JuguetesDTO MapEntity(EJuguetes eJuguetes)
        {
            return new JuguetesDTO
            {
                ID_Juguete = eJuguetes.ID_Juguete,
                Nombre = eJuguetes.Nombre,
                Categoria = eJuguetes.Categoria,
                Tipo = eJuguetes.Tipo,
                Precio = eJuguetes.Precio,
                Stock = eJuguetes.Stock,
                Edad = eJuguetes.Edad,
                Descripcion = eJuguetes.Descripcion
            };
        }

        public void MapDTOUpdate(EJuguetes eJuguetes, JuguetesUpdateDTO juguetesUpdateDTO)
        {
            eJuguetes.Nombre = juguetesUpdateDTO.Nombre;
            eJuguetes.Categoria = juguetesUpdateDTO.Categoria;
            eJuguetes.Tipo = juguetesUpdateDTO.Tipo;
            eJuguetes.Precio = juguetesUpdateDTO.Precio;
            eJuguetes.Stock = juguetesUpdateDTO.Stock;
            eJuguetes.Edad = juguetesUpdateDTO.Edad;
            eJuguetes.Descripcion = juguetesUpdateDTO.Descripcion;
        }
    }
}
