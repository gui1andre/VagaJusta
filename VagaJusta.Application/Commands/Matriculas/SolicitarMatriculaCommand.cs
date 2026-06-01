using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.DTO.Responses;

namespace VagaJusta.Application.Commands.Matriculas
{
    public record SolicitarMatriculaCommand(
        string NomeAluno, 
        string CPFAluno, 
        DateTime DataNascimento, 
        Guid TurmaId) : IRequest<MatriculaResponse>
    {
    }
}
