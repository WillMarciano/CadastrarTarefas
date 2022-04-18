using Xunit;
using CadastrarTarefas.WebApp.Controllers;
using CadastrarTarefas.WebApp.Models;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using CadastrarTarefas.Services.Handlers;
using CadastrarTarefas.Infrastructure;
using CadastrarTarefas.Infrastructure.Extensions;
using CadastrarTarefas.Core.Models;

namespace CadastrarTarefas.Testes
{
    public class TarefasControllerEndPointCadastraTarefa
    {
        [Fact]
        public void CadastrarTarefa()
        {
            //arrange
            var mockLogger = new Mock<ILogger<CadastraTarefaHandler>>();
            var contexto = Conexao.ContextoDbTeste("DbTarefas3");

            contexto.Categorias.Add(new Categoria(20, "Estudo"));
            contexto.SaveChanges();

            var repo = new RepositorioTarefa(contexto);
            var controlador = new TarefasController(repo, mockLogger.Object);
            var model = new CadastraTarefaVM
            {
                IdCategoria = 20,
                Titulo = "Estudar xUnit",
                Prazo = new DateTime(2022, 04, 18)
            };

            //act
            var retorno = controlador.EndpointCadastraTarefa(model);

            //assert
            Assert.IsType<OkResult>(retorno); //200
        }

        [Fact]
        public void ExceptionCadastrarTarefa()
        {
            //arrange
            var mockLogger = new Mock<ILogger<CadastraTarefaHandler>>();

            var mock = new Mock<IRepositorioTarefas>();
            mock.Setup(r => r.ObtemCategoriaPorId(20)).Returns(new Categoria(20, "Estudo"));
            mock.Setup(r => r.IncluirTarefas(It.IsAny<Tarefa[]>())).Throws(new Exception("Houve um erro"));
            var repo = mock.Object;

            var controlador = new TarefasController(repo, mockLogger.Object);
            var model = new CadastraTarefaVM
            {
                IdCategoria = 20,
                Titulo = "Estudar xUnit",
                Prazo = new DateTime(2022, 04, 18)
            };

            //act
            var retorno = controlador.EndpointCadastraTarefa(model);

            //assert
            Assert.IsType<StatusCodeResult>(retorno);
            var statusCode = (retorno as StatusCodeResult).StatusCode;
            Assert.Equal(500, statusCode);
        }
    }
}
