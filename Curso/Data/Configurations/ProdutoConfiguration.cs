﻿using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursoEFCore.Data.Configurations
{
    class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {        
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");
            builder.Property(p => p.Id);
            builder.Property(p => p.CodigoBarras).HasColumnType("VARCHAR(14)").IsRequired();
            builder.Property(P => P.Descricao).HasColumnType("VARCHAR(60)");
            builder.Property(p => p.Valor).IsRequired();
            builder.Property(p => p.TipoProduto).HasConversion<string>();
        }
    }
}
