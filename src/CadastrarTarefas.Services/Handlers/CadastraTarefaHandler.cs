using CadastrarTarefas.Core.Commands;
using CadastrarTarefas.Core.Models;
using CadastrarTarefas.Infrastructure;
using Microsoft.Extensions.Logging;

namespace CadastrarTarefas.Services.Handlers
{
    public class CadastraTarefaHandler
    {
        IRepositorioTarefas _repo;
        ILogger<CadastraTarefaHandler> _logger;

        public CadastraTarefaHandler(IRepositorioTarefas respositorio)
        {
            _repo = respositorio;
            _logger = new LoggerFactory().CreateLogger<CadastraTarefaHandler>();
        }

        public void Execute(CadastraTarefa comando)
        {
            var tarefa = new Tarefa
            (
                id: 0,
                titulo: comando.Titulo,
                prazo: comando.Prazo,
                categoria: comando.Categoria,
                concluidaEm: null,
                status: StatusTarefa.Criada
            );
            _logger.LogDebug("Persistindo a tarefa...");
            _repo.IncluirTarefas(tarefa);
        }
    }
}
