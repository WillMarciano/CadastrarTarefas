using CadastrarTarefas.Core.Models;
using MediatR;

namespace CadastrarTarefas.Core.Commands
{
    public class ObtemCategoriaPorId : IRequest<Categoria>
    {
        public int IdCategoria { get; }
        public ObtemCategoriaPorId(int idCategoria) => IdCategoria = idCategoria;
    }
}
