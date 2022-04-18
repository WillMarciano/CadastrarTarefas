using Microsoft.EntityFrameworkCore;

namespace CadastrarTarefas.Infrastructure.Extensions
{
    public static class Conexao
    {
        public static DbTarefasContext ContextoDbTeste(string banco)
        {
            var option = new DbContextOptionsBuilder<DbTarefasContext>()
                                        .UseInMemoryDatabase(banco)
                                        .Options;

            var contexto = new DbTarefasContext(option);

            return contexto;
        }
    }
}
