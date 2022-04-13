using CadastrarTarefas.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastrarTarefas.Infrastructure
{
    public class DbTarefasContext : DbContext
    {
        public DbTarefasContext(DbContextOptions options) : base(options) { }

        public DbTarefasContext() { }

        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}
