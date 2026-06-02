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
    public class MatriculaConfiguration : IEntityTypeConfiguration<Matricula>
    {
        public void Configure(EntityTypeBuilder<Matricula> builder)
        {
            builder.ToTable("matriculas");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Status).IsRequired();
            builder.Property(m => m.DataSolicitacao).IsRequired();
            builder.Property(m => m.MotivoRejeicao).HasMaxLength(500);

            builder.HasOne(m => m.Aluno).WithMany(a => a.Matriculas).HasForeignKey(m => m.AlunoId);
            builder.HasOne(m => m.Turma).WithMany(t => t.Matriculas).HasForeignKey(m => m.TurmaId);
        }
    }
}
