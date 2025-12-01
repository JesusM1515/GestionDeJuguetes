using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Base;
using Infraestructure.ContextDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories.API
{
    public class UserRepository : BaseRepository<EUsuarios>, IUserRepository
    {
        private readonly Context _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(Context context, ILogger<UserRepository> logger) : base(context, logger)
        {
            this._context = context;
            this._logger = logger;
        }

        //Crear
        public override async Task<EUsuarios> CreateAsync(EUsuarios eUsuarios)
        {
            _logger.LogInformation("Intentando crear usuario");

            await base.CreateAsync(eUsuarios);
            return eUsuarios;
        }

        //Borrar
        public override async Task DeleteIntAsync(int id)
        {
            _logger.LogInformation("Intentando eliminar usuario");

            await base.DeleteIntAsync(id);
        }

        //Traer
        public override async Task<IEnumerable<EUsuarios>> GetAllAsync()
        {
            _logger.LogInformation("Intentando traer la lista de usuarios");

            return await base.GetAllAsync();
        }

        //Traer por id
        public override async Task<EUsuarios?> GetByIdIntAsync(int id)
        {
            _logger.LogInformation("Intentando traer usuario en especifico");

            return await base.GetByIdIntAsync(id);
        }

        //Traer usuario segun su correo
        public async Task<EUsuarios> GetByCorreo(string correo)
        {
            _logger.LogInformation($"Intentando buscar usuario con correo {correo}", correo);

            return await _context.DimUsuarios.FirstOrDefaultAsync(u => u.Correo == correo);
        }

        //Actualizar
        public override async Task<EUsuarios> UpdateAsync(EUsuarios eUsuarios)
        {
            _logger.LogInformation("Intentando actualizar informacion de usuario");

            await base.UpdateAsync(eUsuarios);
            return eUsuarios;
        }
    }
}
