using DesafioProgramacao.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DesafioProgramacao.Infra.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; } 
        public DbSet<Provider> Providers { get; set; }
    }
}
