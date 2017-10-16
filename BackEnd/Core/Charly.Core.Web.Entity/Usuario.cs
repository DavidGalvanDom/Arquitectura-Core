using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Charly.Core.Web.Entity
{
    public class Usuario
    {
        [Required]
        public int UsuarioId { get; set; }

        [Required]
        [MaxLength(40)]
        public string Nombre { get; set; }

        [MaxLength(1)]
        public string Genero { get; set; }

        [MaxLength(20)]
        public string Telefono { get; set; }

        [MaxLength(150)]
        public string Email { get; set; }

        public int PerfilId { get; set; }

        public Perfil Perfil { get; set; }
    }
}
