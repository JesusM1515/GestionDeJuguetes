using Application.Interfaces;
using Domain.Base;
using Infraestructure.ContextDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly Context _context;
        private readonly ILogger<BaseRepository<TEntity>> _logger;
        private DbSet<TEntity> Entities { get; set; }

        public BaseRepository(Context context, ILogger<BaseRepository<TEntity>> logger)
        {
            this._context = context;
            this.Entities = _context.Set<TEntity>();
            this._logger = logger;
        }

        //Añadir
        public virtual async Task CreateAsync(TEntity entity)
        {
            try
            {
                _logger.LogInformation("Itentando añadir datos");

                await Entities.AddAsync(entity);
                await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar a la base de datos");
            }
        }

        //Actualizar
        public virtual async Task UpdateAsync(TEntity entity)
        {
            try
            {
                _logger.LogInformation("Itentando actualizar datos");

                Entities.Update(entity);
                await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar a la base de datos");
            }
        }

        //Traer todo
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            _logger.LogInformation("Itentando traer todos los datos");

            return await Entities.ToListAsync();
        }

        //Traer por id int
        public virtual async Task<TEntity?> GetByIdIntAsync(int id)
        {
            _logger.LogInformation("Itentando traer dato especifico");

            return await Entities.FindAsync(id);
        }

        //Eliminar por id int
        public virtual async Task DeleteIntAsync(int id)
        {
            try
            {
                _logger.LogInformation("Itentando borrar dato especifico");

                var eliminar = await Entities.FindAsync(id);

                if (eliminar != null)
                {
                    Entities.Remove(eliminar);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _logger.LogWarning("No se encontro el registro con ID {Id}", id);
                }
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar dato de la base de datos");
            }
        }
    }
}
