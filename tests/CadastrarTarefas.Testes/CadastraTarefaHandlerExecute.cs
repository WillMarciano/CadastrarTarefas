using CadastrarTarefas.Core.Commands;
using CadastrarTarefas.Core.Models;
using CadastrarTarefas.Infrastructure;
using CadastrarTarefas.Services.Handlers;
using CadastrarTarefas.Testes.Configure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace CadastrarTarefas.Testes
{
    public class CadastraTarefaHandlerExecute
    {
        [Fact]
        public void IncluiraDbAoValidarInformações()
        {
            //arrange
            var comando = new CadastraTarefa("Estudar xUnit", new Categoria("Estudo"), new DateTime(2022, 04, 14));


            var repo = new RepositorioTarefa(Conexao.ContextoDbTeste("DbTarefas1"));
            //var repo = new RepositorioFake();

            var handler = new CadastraTarefaHandler(repo);

            //act
            handler.Execute(comando);

            //assert
            var tarefa = repo.ObtemTarefas(t => t.Titulo == "Estudar xUnit").FirstOrDefault();
            Assert.NotNull(tarefa);

        }
    }
}
