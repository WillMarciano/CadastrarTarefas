using System;

namespace CadastrarTarefas.Core.Commands
{
    public class GerenciaPrazoDasTarefas
    {
        public DateTime DataHoraAtual { get; }
        public GerenciaPrazoDasTarefas() => DataHoraAtual = DateTime.Now;
    }
}
