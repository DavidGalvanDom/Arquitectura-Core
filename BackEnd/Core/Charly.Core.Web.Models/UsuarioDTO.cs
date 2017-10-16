using System;

namespace Charly.Core.Web.Models
{
    public class UsuarioDTO
    {
        public int UsuarioId { get; set; }

        public string Nombre { get; set; }

        public string Genero { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }
    }
}
