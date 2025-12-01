using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository : IBaseRepository<EUsuarios>
    {
        //Buscar por nombre de usuario
        Task<EUsuarios> GetByCorreo(string correo);
    }
}
