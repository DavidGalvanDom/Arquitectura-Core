using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Charly.Core.Web.Entity
{
    public class Perfil
    {
        [Required]
        public int PerfilId { get; set; }

        [MaxLength(50)]
        public string Nombre { get; set; }
    }
}
