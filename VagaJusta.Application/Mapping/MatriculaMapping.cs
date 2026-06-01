using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.DTO.Responses;
using VagaJusta.Domain.Entities;

namespace VagaJusta.Application.Mapping
{
    public static class MatriculaMapping
    {
        public static MatriculaResponse ToResponse(this Matricula matricula) =>
            new MatriculaResponse(
                matricula.Id, 
                matricula.Aluno?.Nome ?? string.Empty, 
                matricula.Aluno?.CPF.Numero ?? string.Empty, 
                $"{nameof(matricula.Turma.Serie)} {nameof(matricula.Turma.CategoriaSerie)}", 
                matricula.Turma?.Escola?.Nome ?? string.Empty, 
                nameof(matricula.Status), 
                matricula.DataSolicitacao);
    }
}
