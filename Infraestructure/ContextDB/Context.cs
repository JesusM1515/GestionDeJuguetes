using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.ContextDB
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> dbContext) : base(dbContext)
        { 
        
        }

        public DbSet<EJuguetes> DimJuguetes { get; set; }
        public DbSet<EUsuarios> DimUsuarios { get; set; }
    }
}
