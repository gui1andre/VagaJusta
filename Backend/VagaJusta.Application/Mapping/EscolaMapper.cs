using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.DTO.Responses;
using VagaJusta.Domain.Entities;

namespace VagaJusta.Application.Mapping
{
    public static class EscolaMapper
    {
        public static EscolaResponse ToResponse(this Escola escola ) =>
            new EscolaResponse(escola.Id, escola.Nome, escola.Endereco, escola.Turmas.Select(TurmaMapping.ToResponse));
    }
}
