using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Domain.Exceptions;
using VagaJusta.Domain.ValueObjects;

namespace VagaJusta.Domain.Entities
{
    public class Aluno : BaseEntity
    {
        public string Nome { get; private set; } = string.Empty;
        public CPF CPF { get; private set; }
        public DateTime DataNascimento { get; private set; }
        private readonly List<Matricula> _matriculas = new();
        public IReadOnlyCollection<Matricula> Matriculas => _matriculas.AsReadOnly();
        public int Idade => CalcularIdade();
        private Aluno() { }

        public static Aluno Criar(string nome, string cpf, DateTime dataNascimento)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new DomainException("O nome do aluno é obrigatório.");

            var aluno = new Aluno
            {
                Nome = nome,
                CPF = new CPF(cpf),
                DataNascimento = dataNascimento
            };
            return aluno;
        }
        private int CalcularIdade()
        {
            var hoje = DateTime.Today;
            var idade = hoje.Year - DataNascimento.Year;
            if (DataNascimento.Date > hoje.AddYears(-idade)) idade--;
            return idade;
        }

        public void Atualizar(string nome, DateTime dataNascimento) 
        {
            if(string.IsNullOrWhiteSpace(nome))
                throw new DomainException("O nome do aluno é obrigatório.");

            Nome = nome;
            DataNascimento = dataNascimento;
        }

    }
}
