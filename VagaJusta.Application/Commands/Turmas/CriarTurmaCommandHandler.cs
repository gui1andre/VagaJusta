using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.DTO.Responses;
using VagaJusta.Application.Exceptions;
using VagaJusta.Application.Mapping;
using VagaJusta.Domain.Entities;
using VagaJusta.Domain.Enums;
using VagaJusta.Domain.Interfaces.Repositories;

namespace VagaJusta.Application.Commands.Turmas
{
    public class CriarTurmaCommandHandler(IEscolaRepository escolaRepository, ITurmaRepository turmaRepository) : IRequestHandler<CriarTurmaCommand, TurmaResponse>
    {
        private readonly ITurmaRepository _turmaRepository = turmaRepository;
        private readonly IEscolaRepository _escolaRepository = escolaRepository;

        public async Task<TurmaResponse> Handle(CriarTurmaCommand request, CancellationToken cancellationToken)
        {
            var escola = await _escolaRepository.ObterPorIdAsync(request.EscolaId, cancellationToken);

            if (escola == null)
                throw new NotFoundException("Escola não encontrada.");

            Enum.TryParse<SerieEnum>(request.Serie, out var serie);
            Enum.TryParse<CategoriaSerieEnum>(request.CategoriaSerie, out var categoriaSerie);

            var turmaExistente = await _turmaRepository.VerificarTurmaJaExistenteAsync(escola.Id, serie, categoriaSerie, cancellationToken);

            if(turmaExistente)
                throw new InvalidOperationException("Já existe uma turma com a mesma série e categoria para esta escola.");

            var turma = Turma.Criar(
                escola.Id, 
                serie,
                categoriaSerie,
                request.CapacidadeMaxima,
                request.IdadeMinima, 
                request.IdadeMaxima);


            await _turmaRepository.AdicionarAsync(turma, cancellationToken);

            return turma.ToResponse();
        }
    }
}
