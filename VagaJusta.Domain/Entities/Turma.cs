using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Domain.Enums;
using VagaJusta.Domain.Exceptions;

namespace VagaJusta.Domain.Entities
{
    public class Turma : BaseEntity
    {
        public Guid EscolaId { get; private set; }
        //public Escola? Escola { get; private set; }
        public SerieEnum Serie { get; private set; }
        public CategoriaSerieEnum CategoriaSerie { get; private set; }
        public int CapacidadeMaxima { get; private set; }
        public int QuantidadeAlunos { get; private set; }
        public int IdadeMinima { get; private set; }
        public int IdadeMaxima { get; private set;  }
        public int VagasDisponiveis => CapacidadeMaxima - QuantidadeAlunos;
        public bool TurmaLotada => QuantidadeAlunos >= CapacidadeMaxima;
        private readonly List<Matricula> _matriculas = [];
        public IReadOnlyCollection<Matricula> Matriculas => _matriculas.AsReadOnly();

        protected Turma() { }

        public static Turma Criar(Guid escolaId, SerieEnum serie, CategoriaSerieEnum categoriaSerie, int capacidadeMaxima, int idadeMinima, int idadeMaxima)
        {
            if(capacidadeMaxima <= 0)
                throw new DomainException("A capacidade máxima deve ser maior que zero.");

            if(idadeMinima <= 0)
                throw new DomainException("A idade mínima deve ser maior que zero.");

            if(idadeMaxima < idadeMinima)
                throw new DomainException("A idade máxima deve ser maior ou igual à idade mínima.");

            return new Turma
            {
                EscolaId = escolaId,
                Serie = serie,
                CategoriaSerie = categoriaSerie,
                CapacidadeMaxima = capacidadeMaxima,
                IdadeMinima = idadeMinima,
                IdadeMaxima = idadeMaxima
            };
        }

        public bool IdadeAlunoCompativel(int idade)
        {
            return idade >= IdadeMinima && idade <= IdadeMaxima;
        }

        public void IncrementarVagasOcupadas()
        {
            if (TurmaLotada)
                throw new DomainException("Turma sem vagas disponíveis.");
            QuantidadeAlunos++;
        }
    }
}
