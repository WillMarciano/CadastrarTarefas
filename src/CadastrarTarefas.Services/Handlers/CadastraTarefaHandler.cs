using CadastrarTarefas.Core.Commands;
using CadastrarTarefas.Core.Models;
using CadastrarTarefas.Infrastructure;
using Microsoft.Extensions.Logging;
using System;

namespace CadastrarTarefas.Services.Handlers
{
    public class CadastraTarefaHandler
    {
        IRepositorioTarefas _repo;
        ILogger<CadastraTarefaHandler> _logger;

        public CadastraTarefaHandler(IRepositorioTarefas respositorio, ILogger<CadastraTarefaHandler> logger)
        {
            _repo = respositorio;
            _logger = logger;
        }

        public CommandResult Execute(CadastraTarefa comando)
        {
            try
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
                _logger.LogDebug($"Persistindo a tarefa {tarefa.Titulo}");
                _repo.IncluirTarefas(tarefa);
                return new CommandResult(true);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new CommandResult(false);
            }

        }
    }
}
