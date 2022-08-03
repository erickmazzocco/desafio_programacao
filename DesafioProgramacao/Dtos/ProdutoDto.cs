using DesafioProgramacao.Entities;
using System;

namespace DesafioProgramacao.Dtos
{
    public class ProdutoDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Status { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        
        public Fornecedor Fornecedor { get; set; }
    }

    public class ProdutoCreateDto
    {
        public string Descricao { get; set; }
        public bool Status { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }

        public int FornecedorId { get; set; }
    }

    public class ProdutoUpdateDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Status { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }

        public int FornecedorId { get; set; }
    }
}
