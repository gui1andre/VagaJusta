using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Domain.Exceptions;

namespace VagaJusta.Domain.Entities
{
    public class Escola : BaseEntity
    {
        public string Nome { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;


        private readonly List<Turma> _turmas = [];
        public IReadOnlyCollection<Turma> Turmas => _turmas.AsReadOnly();

        protected Escola() { }

        public static Escola Criar(string nome, string endereco)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new DomainException("Nome da escola é obrigatório.");
            if (string.IsNullOrWhiteSpace(endereco))
                throw new DomainException("Endereço da escola é obrigatório.");

            return new Escola
            {
                Nome = nome.Trim(),
                Endereco = endereco.Trim()
            };
        }

        public void Atualizar(string nome, string endereco)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new DomainException("Nome da escola é obrigatório.");
            if (string.IsNullOrWhiteSpace(endereco))
                throw new DomainException("Endereço da escola é obrigatório.");

            Nome = nome.Trim();
            Endereco = endereco.Trim();
        }

        public void AdicionarTurma(Turma turma)
        {
            if (_turmas.Any(t => t.Serie == turma.Serie && t.CategoriaSerie == turma.CategoriaSerie))
                throw new DomainException($"Já existe uma turma da serie {turma.Serie} na categoria {turma.CategoriaSerie}.");

            _turmas.Add(turma);
        }
    }
}
