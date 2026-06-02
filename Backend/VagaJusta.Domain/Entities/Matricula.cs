using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Domain.Enums;
using VagaJusta.Domain.Exceptions;

namespace VagaJusta.Domain.Entities
{
    public class Matricula : BaseEntity
    {
        public Guid AlunoId { get; private set; }
        public Aluno? Aluno { get; private set; }
        public Guid TurmaId { get; private set; }
        public Turma? Turma { get; private set; }
        public StatusMatriculaEnum Status { get; private set; }
        public DateTime DataSolicitacao { get; private set;  }
        public string? MotivoRejeicao { get; private set; }

        protected Matricula() { }

        public static Matricula Criar(Guid alunoId, Guid turmaId)
        {
            return new Matricula
            {
                AlunoId = alunoId,
                TurmaId = turmaId,
                Status = StatusMatriculaEnum.AguardandoProcessamento,
                DataSolicitacao = DateTime.UtcNow
            };
        }

        public void Aprovar()
        {
            if (Status == StatusMatriculaEnum.Aprovada)
                throw new DomainException("A matrícula já está aprovada.");

            if (Status != StatusMatriculaEnum.AguardandoProcessamento)
                throw new DomainException("A matrícula só pode ser aprovada se estiver aguardando processamento.");

            Status = StatusMatriculaEnum.Aprovada;
        }

        public void Rejeitar(string motivo)
        {
            if (string.IsNullOrWhiteSpace(motivo))
                throw new DomainException("O motivo da rejeição é obrigatorio.");

            Status = StatusMatriculaEnum.Rejeitada;
            MotivoRejeicao = motivo;
        }

        public void ColocarEmEspera() => Status = StatusMatriculaEnum.AguardandoVaga;
        
    }
}
