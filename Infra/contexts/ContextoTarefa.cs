using Domain.models;
using Infra.configurations.entitymaps;
using Microsoft.EntityFrameworkCore;

namespace Infra.contexts
{
    public class ContextoTarefa : DbContext
    {
        public ContextoTarefa(DbContextOptions<ContextoTarefa> options) : base(options)
        {
        }
        public DbSet<Tarefa> Tarefas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("task");
            modelBuilder.ApplyConfiguration(new ConfiguracaoTarefa());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextoTarefa).Assembly);
        }
    }
}
