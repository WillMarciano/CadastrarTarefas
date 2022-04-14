using CadastrarTarefas.Core.Commands;
using CadastrarTarefas.Core.Models;
using CadastrarTarefas.Infrastructure;
using CadastrarTarefas.Services.Handlers;
using CadastrarTarefas.Testes.Configure;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CadastrarTarefas.Testes
{
    public class GerenciaPrazoDasTarefasHandlerExecute
    {
        [Fact]
        public void TarefasAtrasadasDevemMudarSeuStatus()
        {
            //arrange
            var compCateg = new Categoria(1, "Compras");
            var casaCateg = new Categoria(2, "Casa");
            var trabCateg = new Categoria(3, "Trabalho");
            var saudCateg = new Categoria(4, "Saúde");
            var higiCateg = new Categoria(5, "Higiene");

            var tarefas = new List<Tarefa>
            {
                //Atrasadas
                new Tarefa(1, "Tirar Lixo",           casaCateg, new DateTime(2022,01,01), null, StatusTarefa.Criada),
                new Tarefa(2, "Fazer Almoçco",        casaCateg, new DateTime(2022,01,01), null, StatusTarefa.Criada),
                new Tarefa(3, "Ir à academia",        saudCateg, new DateTime(2022,01,01), null, StatusTarefa.Criada),
                new Tarefa(4, "Concluir o relatório", trabCateg, new DateTime(2022,01,01), null, StatusTarefa.Criada),
                new Tarefa(5, "beber água",           saudCateg, new DateTime(2022,01,01), null, StatusTarefa.Criada),
                //Dentro do prazo
                new Tarefa(6, "Comprar presente",     compCateg, new DateTime(2023,01,01), null, StatusTarefa.Criada),
                new Tarefa(7, "Escovar os dentes",    saudCateg, new DateTime(2024,01,01), null, StatusTarefa.Criada)
            };

            var repo = new RepositorioTarefa(Conexao.ContextoDbTeste("DbTarefas2"));
            repo.IncluirTarefas(tarefas.ToArray());

            var comando = new GerenciaPrazoDasTarefas(new DateTime(2022, 4, 14));
            var handler = new GerenciaPrazoDasTarefasHandler(repo);

            //act
            handler.Execute(comando);

            //assert
            var tarefasEmAtraso = repo.ObtemTarefas(t => t.Status == StatusTarefa.EmAtraso);
            Assert.Equal(5, tarefasEmAtraso.Count());
        }

        [Fact]
        public void VerificarQuantidadeDeVezesTotalTarefasAtrasadas()
        {
            //arrange
            var categ = new Categoria("Dummy");
            var tarefas = new List<Tarefa>
            {
                new Tarefa(1, "Tirar Lixo",    categ, new DateTime(2022,01,01), null, StatusTarefa.Criada),
                new Tarefa(2, "Fazer Almoçco", categ, new DateTime(2022,01,01), null, StatusTarefa.Criada),
                new Tarefa(3, "Ir à academia", categ, new DateTime(2022,01,01), null, StatusTarefa.Criada),
            };

            var mock = new Mock<IRepositorioTarefas>();
            mock.Setup(r => r.ObtemTarefas(It.IsAny<Func<Tarefa, bool>>()))
                .Returns(tarefas);
            var repo = mock.Object;

            var comando = new GerenciaPrazoDasTarefas(new DateTime(2022, 4, 14));
            var handler = new GerenciaPrazoDasTarefasHandler(repo);

            //act
            handler.Execute(comando);

            //assert
            mock.Verify(r => r.AtualizarTarefas(It.IsAny<Tarefa[]>()), Times.Once());
        }
    }
}
