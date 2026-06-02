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
    public class TurmaConfiguration : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.ToTable("turmas");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Serie).IsRequired();
            builder.Property(x => x.CategoriaSerie).IsRequired();
            builder.Property(x => x.CapacidadeMaxima).IsRequired();
            builder.Property(x => x.QuantidadeAlunos).IsRequired();
            builder.Property(x => x.IdadeMinima).IsRequired();
            builder.Property(x => x.IdadeMaxima).IsRequired();
        }
    }
}
