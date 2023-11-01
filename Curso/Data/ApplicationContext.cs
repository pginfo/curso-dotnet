using Microsoft.EntityFrameworkCore;
using CursoEFCore.Domain;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;

namespace CursoEFCore.Data
{
    class ApplicationContext: DbContext
    {
        private static readonly ILoggerFactory _logger = LoggerFactory.Create(p => p.AddConsole());

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(_logger)
                .EnableSensitiveDataLogging()
                .UseSqlServer("Data source=(localdb)\\mssqllocaldb;Initial Catalog=CursoEFCore;Integrated Security=true",
                p=>p.EnableRetryOnFailure(
                    maxRetryCount: 2,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorNumbersToAdd: null
                    ).MigrationsHistoryTable("curso_ef_core"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
             * Irá proocurar por todas as classes 
             * que usaram a interface IEntityTypeConfiguration
             */
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
            MapearPropriedadesEsquecidas(modelBuilder);
        }

        /*
         * Detectando propriedades não configuradas
         */
        private static void MapearPropriedadesEsquecidas(ModelBuilder modelBuilder)
        {
            foreach(var entity in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entity.GetProperties().Where(p => p.ClrType == typeof(string));

                foreach(var property in properties)
                {
                    if(string.IsNullOrEmpty(property.GetColumnType()) && !property.GetMaxLength().HasValue)
                    {
                        property.SetColumnType("VARCHAR(100)");
                    }
                }
            }
        }
    }
}

