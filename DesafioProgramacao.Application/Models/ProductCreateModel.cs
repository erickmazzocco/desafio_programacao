using System;

namespace DesafioProgramacao.Application.Models
{
    public class ProductCreateModel
    {
        public string Description { get; set; }

        public bool Status { get; set; }

        public DateTime ManufacturingDate { get; set; }

        public DateTime ValidationDate { get; set; }

        public int ProviderId { get; set; }
    }
}
