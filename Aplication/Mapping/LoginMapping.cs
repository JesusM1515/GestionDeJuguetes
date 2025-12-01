using Application.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class LoginMapping
    {
        public LoginDTO MapDTO(EUsuarios eUsuarios)
        {
            LoginDTO mapeo = new LoginDTO
            {
                Correo = eUsuarios.Correo
            };

            return mapeo;
        }
    }
}
