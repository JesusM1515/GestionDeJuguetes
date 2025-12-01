using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IJuguetesRepository : IBaseRepository<EJuguetes>
    {
        Task<EJuguetes?> GetByIdStringAsync(string idJuguete);
        Task<IEnumerable<EJuguetes>> GetByNameAsync(string nombre);
        Task<bool> DeleteByStringIdAsync(string idJuguete);
    }
}
