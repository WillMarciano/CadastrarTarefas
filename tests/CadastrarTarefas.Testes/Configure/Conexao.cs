using CadastrarTarefas.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CadastrarTarefas.Testes.Configure
{
    public static class Conexao
    {
        public static DbTarefasContext ContextoDbTeste(string banco)
        {
            var option = new DbContextOptionsBuilder<DbTarefasContext>()
                                        .UseInMemoryDatabase(banco)
                                        .Options;

            return new DbTarefasContext(option);
        }
    }
}
