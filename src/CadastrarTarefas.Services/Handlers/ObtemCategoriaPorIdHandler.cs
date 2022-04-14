using CadastrarTarefas.Core.Commands;
using CadastrarTarefas.Core.Models;
using CadastrarTarefas.Infrastructure;

namespace CadastrarTarefas.Services.Handlers
{
    public class ObtemCategoriaPorIdHandler
    {
        IRepositorioTarefas _repo;

        public ObtemCategoriaPorIdHandler(IRepositorioTarefas repo) => _repo = repo;

        public Categoria Execute(ObtemCategoriaPorId comando) => _repo.ObtemCategoriaPorId(comando.IdCategoria);
    }
}
