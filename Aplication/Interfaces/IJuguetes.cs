using Application.DTO;
using Domain.Base;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IJuguetes
    {
        //Traer juguete por id
        public Task<OperationResult<JuguetesDTO>> GetbyIdasync(string id);

        //Traer juguete por nombre
        public Task<OperationResult<IEnumerable<JuguetesDTO>>> GetbyNameasync(string name);

        //Traer juguete
        public Task<OperationResult<IEnumerable<JuguetesDTO>>> GetAllasync();

        //Añadir juguete
        public Task<OperationResult<JuguetesDTO>> AddJuguete(JuguetesDTO juguetesDTO);

        //Actualizar juguete
        public Task<OperationResult<JuguetesUpdateDTO>> Updateasync(string id, JuguetesUpdateDTO juguetesUpdateDTO);

        //Eliminar juguete
        public Task<OperationResult<bool>> DeleteAsync(string id, bool confirmar);
    }
}
