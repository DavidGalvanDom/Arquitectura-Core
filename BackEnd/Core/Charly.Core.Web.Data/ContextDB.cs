using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using Charly.Core.Web.Entity;

namespace Charly.Core.Web.Data
{
    public class Contextdb : DbContext
    {
        public Contextdb(DbContextOptions<Contextdb> options)
            : base(options)
        { }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Perfil> Perfiles { get; set; }

    }
}
