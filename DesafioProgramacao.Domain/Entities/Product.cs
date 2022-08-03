using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioProgramacao.Domain.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        public string Description { get; set; }        

        public DateTime ManufacturingDate { get; set; }

        public DateTime ValidationDate { get; set; }

        public int ProviderId { get; set; }

        public Provider Provider { get; set; }
    }
}
