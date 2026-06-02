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
    public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.ToTable("alunos");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Nome).HasMaxLength(200).IsRequired();
            builder.OwnsOne(a => a.CPF, cpf =>
            {
                cpf.Property(c => c.Numero).HasColumnName("CPF").HasMaxLength(11).IsRequired();
                cpf.HasIndex(c => c.Numero).IsUnique();
            });
            builder.Property(a => a.DataNascimento).IsRequired();
        }
    }
}
