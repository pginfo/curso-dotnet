using Microsoft.EntityFrameworkCore;

/*
 * Instalar os pacotes
 * Microsoft.EntityFrameworkCore.SqlServer 
 * Microsoft.EntityFrameworkCore.Tools
 */

namespace LabUploadFiles.Models.Domain
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        {               
        }

        public DbSet<Product> Products { get; set; }
    }
}
