using System;

namespace CadastrarTarefas.WebApp.Models
{
    public class CadastraTarefaVM
    {
        public string Titulo { get; set; }
        public int IdCategoria { get; set; }
        public DateTime Prazo { get; set; }
    }
}
