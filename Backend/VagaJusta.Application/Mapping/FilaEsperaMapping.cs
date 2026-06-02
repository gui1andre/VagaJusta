using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.DTO.Responses;
using VagaJusta.Domain.Entities;

namespace VagaJusta.Application.Mapping
{
    public static class FilaEsperaMapping
    {
        public static FilaEsperaResponse ToFilaEsperaResponse(this Matricula matricula, int posicao) =>
            new FilaEsperaResponse(
                posicao,
                matricula.Id,
                matricula.Aluno!.ToResponse(),
                matricula.DataSolicitacao);
    }
}
