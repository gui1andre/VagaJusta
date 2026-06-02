using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.DTO.Responses;
using VagaJusta.Domain.Entities;

namespace VagaJusta.Application.Mapping
{
    public static class AlunoMapping
    {

        public static AlunoResponse ToResponse(this Aluno aluno) =>
            new AlunoResponse(
                aluno.Id,
                aluno.Nome,
                aluno.CPF.Numero,
                aluno.DataNascimento,
                aluno.Idade);
    }
}
