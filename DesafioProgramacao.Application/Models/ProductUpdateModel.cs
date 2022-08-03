using System;

namespace DesafioProgramacao.Application.Models
{
    public class ProductUpdateModel
    {
        public int Id { get; set; }

        public string Description { get; set; }        

        public DateTime ManufacturingDate { get; set; }

        public DateTime ValidationDate { get; set; }

        public int ProviderId { get; set; }
    }
}
