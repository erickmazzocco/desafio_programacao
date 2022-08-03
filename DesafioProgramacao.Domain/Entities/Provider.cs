using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioProgramacao.Domain.Entities
{
    public class Provider : BaseEntity
    {
        [Required]
        public string Description { get; set; }
        
        [Required]
        public string Cnpj { get; set; }
    }
}
