using CadastrarTarefas.Core.Commands;
using CadastrarTarefas.Infrastructure;
using CadastrarTarefas.Services.Handlers;
using Moq;
using Xunit;

namespace CadastrarTarefas.Testes
{
    public class ObtemCategoriaPorIdExecute
    {
        [Fact]
        public void SeIdForExistenteChamarObtemCategoriaPorIdUmaUnicaVez()
        {
            //arrange
            var idCategoria = 20;
            var comando = new ObtemCategoriaPorId(idCategoria);
            var mock = new Mock<IRepositorioTarefas>();
            var repo = mock.Object;
            var handler = new ObtemCategoriaPorIdHandler(repo);

            //act
            handler.Execute(comando);

            //execute
            mock.Verify(r => r.ObtemCategoriaPorId(idCategoria), Times.Once());
        }
    }
}
