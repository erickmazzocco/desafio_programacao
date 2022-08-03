using System;
using System.ComponentModel.DataAnnotations;

namespace DesafioProgramacao.Entities
{
    public class Produto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Descricao { get; set; }

        public bool Status { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }

        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }
    }
}
