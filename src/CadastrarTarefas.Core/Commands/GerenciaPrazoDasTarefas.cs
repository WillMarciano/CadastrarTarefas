using System;

namespace CadastrarTarefas.Core.Commands
{
    public class GerenciaPrazoDasTarefas
    {
        public DateTime DataHoraAtual { get; }
        public GerenciaPrazoDasTarefas(DateTime dataHoraAtual) => DataHoraAtual = dataHoraAtual;
    }
}
