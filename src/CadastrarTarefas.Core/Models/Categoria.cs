﻿namespace CadastrarTarefas.Core.Models
{
    public class Categoria
    {
        public Categoria(string descricao) => Descricao = descricao;

        public Categoria(int id, string descricao) : this(descricao) => Id = id;
        public int Id { get; private set; }
        public string Descricao { get; private set; }
    }
}
