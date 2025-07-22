using appVelas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Data
{
    public class Contexto : DbContext
    {

        public Contexto(DbContextOptions options) : base(options)
        { }
        public DbSet<Cera> Cera { get; set; }
        public DbSet<Pack> Pack { get; set; }
        public DbSet<Endurecedor> Endurecedor { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Costes> Costes { get; set; }
        public DbSet<Fragancia> Fragancia { get; set; }
        public DbSet<Mecha> Mecha { get; set; }
        public DbSet<Molde> Molde { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Pigmento> Pigmento { get; set; }
        public DbSet<Vela> Vela { get; set; }

    }
}
