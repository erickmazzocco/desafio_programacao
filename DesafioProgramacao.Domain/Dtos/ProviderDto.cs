using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioProgramacao.Domain.Dtos
{
    public class ProviderDto
    {
        public int Id { get; set; }

        public bool Status { get; set; }
        
        public string Description { get; set; }
        
        public string Cnpj { get; set; }
    }
}
