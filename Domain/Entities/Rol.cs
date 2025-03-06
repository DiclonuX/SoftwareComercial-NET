using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }
        public required string Nombre { get; set; } 
    }
}
