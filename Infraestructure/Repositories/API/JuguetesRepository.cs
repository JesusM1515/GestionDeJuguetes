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
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infraestructure.Repositories.API
{
    public class JuguetesRepository : BaseRepository<EJuguetes>, IJuguetesRepository
    {
        private readonly Context _context;
        private readonly ILogger<JuguetesRepository> _logger;

        public JuguetesRepository(Context context, ILogger<JuguetesRepository> logger) : base(context, logger)
        {
            this._context = context;
            this._logger = logger;
        }

        //Crear
        public override async Task<EJuguetes> CreateAsync(EJuguetes eJuguetes)
        {
            _logger.LogInformation("Intentando añadir juguete");

            await base.CreateAsync(eJuguetes);
            return eJuguetes;
        }

        //Borrar
        public async Task<bool> DeleteByStringIdAsync(string idJuguete)
        {
            var entity = await _context.DimJuguetes.FirstOrDefaultAsync(j => j.ID_Juguete == idJuguete);

            if (entity == null)
                return false;

            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        //Traer
        public override async Task<IEnumerable<EJuguetes>> GetAllAsync()
        {
            _logger.LogInformation("Intentando traer la lista de juguetes");

            return await base.GetAllAsync();
        }

        //Traer por id
        public async Task<EJuguetes?> GetByIdStringAsync(string idJuguete)
        {
            _logger.LogInformation("Intentando traer juguetes con el id {id}", idJuguete);
            return await _context.DimJuguetes.FirstOrDefaultAsync(j => j.ID_Juguete == idJuguete);
        }

        //Traer por nombre
        public async Task<IEnumerable<EJuguetes>> GetByNameAsync(string nombre)
        {
            _logger.LogInformation("Intentando buscar juguetes con el nombre {Nombre}", nombre);

            return await _context.DimJuguetes.Where(u => u.Nombre == nombre).ToListAsync();
        }

        //Actualizar
        public override async Task UpdateAsync(EJuguetes eJuguetes)
        {
            _logger.LogInformation("Intentando actualizar informacion de Juguete");

            await base.UpdateAsync(eJuguetes);
        }
    }
}
