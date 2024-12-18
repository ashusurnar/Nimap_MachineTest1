using Microsoft.EntityFrameworkCore;
using Nimap_MachineTest1.Models;

namespace Nimap_MachineTest1.DAL
{
    public class ApplicationContext :DbContext
    {
        public ApplicationContext()
        {
        }

        public ApplicationContext( DbContextOptions options):base (options)
        {
            
        }

        public DbSet<Category> categories { get; set; }

        public DbSet<Product> products { get; set; }
    }
}
