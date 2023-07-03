using ExDDD.DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExDDD.AppConsoleAp.Infra
{
    internal class MyContext : DbContext
    {
        public MyContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=ExDDD;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VendaMap());
            modelBuilder.ApplyConfiguration(new CotacaoMap());
            modelBuilder.ApplyConfiguration(new AporteMap());
            modelBuilder.ApplyConfiguration(new ProventoMap());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Ativo> Ativos { get; set; }
        public DbSet<Aporte> Aportes { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Provento> Proventos { get; set; }
        public DbSet<Cotacao> Cotacoes { get; set; }
    }
}
