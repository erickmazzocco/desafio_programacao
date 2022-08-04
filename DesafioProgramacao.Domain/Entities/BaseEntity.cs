using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioProgramacao.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        [Required]
        public virtual int Id { get; set; }

        public bool Status { get; set; }

        public string Description { get; set; }
    }
}
