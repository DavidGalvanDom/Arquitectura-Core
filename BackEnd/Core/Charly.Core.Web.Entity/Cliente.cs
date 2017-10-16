using System;
using System.ComponentModel.DataAnnotations;

namespace Charly.Core.Web.Entity
{
    public class Cliente
    {
        [Required]
        public int ClienteId { get; set; }

        [MaxLength(50)]
        public string Nombre { get; set; }

        [MaxLength(250)]
        public string Direccion { get; set; }

    }
}
