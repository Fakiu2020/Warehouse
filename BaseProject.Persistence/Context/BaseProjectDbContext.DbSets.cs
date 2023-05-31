using Microsoft.EntityFrameworkCore;
using BaseProject.Domain;

namespace BaseProject.Persistence
{
    public partial class BaseProjectDbContext
    {
  
        public virtual DbSet<Warehouse> Warehouse { get; set; }




    }
}
