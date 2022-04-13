using CadastrarTarefas.Core.Commands;
using CadastrarTarefas.Core.Models;
using CadastrarTarefas.Infrastructure;

namespace CadastrarTarefas.Services.Handlers
{
    public class ObtemCategoriaPorIdHandler
    {
        IRepositorioTarefas _repo;

        public ObtemCategoriaPorIdHandler() => _repo = new RepositorioTarefa();
        public Categoria Execute(ObtemCategoriaPorId comando) => _repo.ObtemCategoriaPorId(comando.IdCategoria);
    }
}
