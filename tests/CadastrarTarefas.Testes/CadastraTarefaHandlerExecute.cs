using CadastrarTarefas.Core.Commands;
using CadastrarTarefas.Core.Models;
using CadastrarTarefas.Infrastructure;
using CadastrarTarefas.Infrastructure.Extensions;
using CadastrarTarefas.Services.Handlers;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
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

            var mockLogger = new Mock<ILogger<CadastraTarefaHandler>>();

            var handler = new CadastraTarefaHandler(repo, mockLogger.Object);

            //act
            handler.Execute(comando);

            //assert
            var tarefa = repo.ObtemTarefas(t => t.Titulo == "Estudar xUnit").FirstOrDefault();
            Assert.NotNull(tarefa);

        }

        [Fact]
        public void DadaTarefaComInfoValidasDeveLogar()
        {
            //arrange            
            var tituloTarefaEsperado = "Usar Moq para aprofundar conhecimento de API";
            var comando = new CadastraTarefa(tituloTarefaEsperado, new Categoria(100, "Estudo"), new DateTime(2019, 12, 31));

            var mockLogger = new Mock<ILogger<CadastraTarefaHandler>>();

            LogLevel levelCapturado = LogLevel.Error;
            string mensagemCapturada = string.Empty;

            mockLogger.Setup(
                l => l.Log(
                    LogLevel.Debug,
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()))
                .Callback((IInvocation invocation) =>
                {
                    var logLevel = (LogLevel)invocation.Arguments[0];
                    var eventId = (EventId)invocation.Arguments[1];
                    var state = (IReadOnlyCollection<KeyValuePair<string, object>>)invocation.Arguments[2];
                    var exception = invocation.Arguments[3] as Exception;
                    var formatter = invocation.Arguments[4] as Delegate;
                    var formatterStr = formatter.DynamicInvoke(state, exception);

                    levelCapturado = logLevel;
                    mensagemCapturada = formatterStr.ToString();
                });

            var mock = new Mock<IRepositorioTarefas>();

            var handler = new CadastraTarefaHandler(mock.Object, mockLogger.Object);

            //act
            handler.Execute(comando); //SUT >> CadastraTarefaHandlerExecute

            //assert
            Assert.Equal(LogLevel.Debug, levelCapturado);
            Assert.Contains(tituloTarefaEsperado, mensagemCapturada);
        }

        [Fact]
        public void SeHouverExceptionResultadoIsSuccessDeveSerFalso()
        {
            //arrange
            var comando = new CadastraTarefa("Estudar xUnit", new Categoria("Estudo"), new DateTime(2022, 04, 14));

            var mockLogger = new Mock<ILogger<CadastraTarefaHandler>>();

            var mock = new Mock<IRepositorioTarefas>();
            mock.Setup(r => r.IncluirTarefas(It.IsAny<Tarefa[]>()))
                .Throws(new Exception("Houve um erro na inclusao de tarefas"));

            var repo = mock.Object;

            var handler = new CadastraTarefaHandler(repo, mockLogger.Object);

            //act
            CommandResult resultado = handler.Execute(comando);

            //assert
            Assert.False(resultado.IsSuccess);
        }



        [Fact]
        public void SeHouverExceptionDeveLogarMensagemDaExcecao()
        {
            //arrange
            var mensagemErro = "Houve um erro na inclusao de tarefas";
            var excecaoEsperada = new Exception(mensagemErro);
            var comando = new CadastraTarefa("Estudar xUnit", new Categoria("Estudo"), new DateTime(2022, 04, 14));

            var mockLogger = new Mock<ILogger<CadastraTarefaHandler>>();

            var mock = new Mock<IRepositorioTarefas>();
            mock.Setup(r => r.IncluirTarefas(It.IsAny<Tarefa[]>()))
                .Throws(excecaoEsperada);

            var repo = mock.Object;

            var handler = new CadastraTarefaHandler(repo, mockLogger.Object);

            //act
            CommandResult resultado = handler.Execute(comando);

            //assert
            mockLogger.Verify(l =>
                l.Log(
                        LogLevel.Error,                                         //nível de log => LogError
                        It.IsAny<EventId>(),                                    //Identificador do evento
                        It.IsAny<object>(),                                     //Objeto que será logado
                        excecaoEsperada,                                        //exceção que será logada
                        (Func<object, Exception, string>)It.IsAny<object>()),   //função que converte objeto+exceção >> string
                        Times.Once()
                    );
        }
    }
}
