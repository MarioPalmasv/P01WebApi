using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace P01WebApi.Models
{
    public class tiendaContext : DbContext
    {
        public tiendaContext (DbContextOptions<tiendaContext> options) : base(options)
        {

        }

        public DbSet<categoria> categoria { get; set; }
    }
}
