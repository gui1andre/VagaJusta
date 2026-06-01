using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.DTO.Responses;
using VagaJusta.Domain.Entities;

namespace VagaJusta.Application.Mapping
{
    public static class TurmaMapping
    {
        public static TurmaResponse ToResponse(Turma turma) =>
            new TurmaResponse(turma.Id, nameof(turma.Serie), nameof(turma.CategoriaSerie), turma.CapacidadeMaxima, turma.QuantidadeAlunos, turma.IdadeMinima, turma.IdadeMaxima, turma.VagasDisponiveis);
    }
}
