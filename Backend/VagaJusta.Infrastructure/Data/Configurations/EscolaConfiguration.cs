using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Domain.Entities;

namespace VagaJusta.Infrastructure.Data.Configurations
{
    public class EscolaConfiguration : IEntityTypeConfiguration<Escola>
    {
        public void Configure(EntityTypeBuilder<Escola> builder)
        {
            builder.ToTable("escolas");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Nome).HasMaxLength(200).IsRequired();
            builder.Property(e => e.Endereco).HasMaxLength(300).IsRequired();
            builder.HasMany(e => e.Turmas).WithOne(t => t.Escola).HasForeignKey(t => t.EscolaId);
        }
    }
}
