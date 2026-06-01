using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Domain.Entities;

namespace VagaJusta.Infrastructure.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options) { }

        public DbSet<Escola> Escolas => Set<Escola>();
        public DbSet<Aluno> Alunos => Set<Aluno>();
        public DbSet<Turma> Turma => Set<Turma>();
        public DbSet<Matricula> Matriculas => Set<Matricula>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DBContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
