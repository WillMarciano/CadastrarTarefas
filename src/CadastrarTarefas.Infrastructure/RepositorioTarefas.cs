using CadastrarTarefas.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CadastrarTarefas.Infrastructure
{
    public class RepositorioTarefa : IRepositorioTarefas
    {
        private readonly DbTarefasContext _context;

        public RepositorioTarefa() => _context = new DbTarefasContext();

        public void AtualizarTarefas(params Tarefa[] tarefas)
        {
            _context.Tarefas.UpdateRange(tarefas);
            _context.SaveChanges();
        }

        public void ExcluirTarefas(params Tarefa[] tarefas)
        {
            _context.Tarefas.RemoveRange(tarefas);
            _context.SaveChanges();
        }

        public void IncluirTarefas(params Tarefa[] tarefas)
        {
            _context.Tarefas.AddRange(tarefas);
            _context.SaveChanges();
        }

        public Categoria ObtemCategoriaPorId(int id)
        {
            return _context.Categorias.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Tarefa> ObtemTarefas(Func<Tarefa, bool> filtro)
        {
            return _context.Tarefas.Where(filtro);
        }
    }
}
