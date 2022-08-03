using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioProgramacao.Domain.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        public bool Status { get; set; }

        public string Description { get; set; }

        public DateTime ManufacturingDate { get; set; }

        public DateTime ValidationDate { get; set; }

        public int ProviderId { get; set; }
    }
}
