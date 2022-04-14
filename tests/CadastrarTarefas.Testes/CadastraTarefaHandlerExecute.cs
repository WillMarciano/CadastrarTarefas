using CadastrarTarefas.Core.Commands;
using CadastrarTarefas.Core.Models;
using CadastrarTarefas.Infrastructure;
using CadastrarTarefas.Services.Handlers;
using CadastrarTarefas.Testes.Configure;
using Moq;
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

        [Fact]
        public void SeHouverExceptionResultadoIsSuccessDeveSerFalso()
        {
            //arrange
            var comando = new CadastraTarefa("Estudar xUnit", new Categoria("Estudo"), new DateTime(2022, 04, 14));

            var mock = new Mock<IRepositorioTarefas>();
            mock.Setup(r => r.IncluirTarefas(It.IsAny<Tarefa[]>()))
                .Throws(new Exception("Houve um erro na inclusao de tarefas"));

            var repo = mock.Object;

            var handler = new CadastraTarefaHandler(repo);

            //act
            CommandResult resultado = handler.Execute(comando);

            //assert
            Assert.False(resultado.IsSuccess);
        }
    }
}
