using ExDDD.DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExDDD.AppConsoleAp.Infra
{
    internal class VendaMap : IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            builder.Property(p => p.DtVenda).HasColumnType("Date");
        }
    }

    internal class AporteMap : IEntityTypeConfiguration<Aporte>
    {
        public void Configure(EntityTypeBuilder<Aporte> builder)
        {
            builder.Property(p => p.DtCompra).HasColumnType("Date");
        }
    }

    internal class CotacaoMap : IEntityTypeConfiguration<Cotacao>
    {
        public void Configure(EntityTypeBuilder<Cotacao> builder)
        {
            builder.Property(p => p.Data).HasColumnType("Date");
            builder.Property(p => p.DataInclusao).HasColumnType("Date");
        }
    }

    internal class ProventoMap : IEntityTypeConfiguration<Provento>
    {
        public void Configure(EntityTypeBuilder<Provento> builder)
        {
            builder.Property(p => p.Data).HasColumnType("Date");
        }
    }
}
