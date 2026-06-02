using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.Commands.Turmas;
using VagaJusta.Application.Validators.Rules;
using VagaJusta.Domain.Enums;

namespace VagaJusta.Application.Validators
{
    public class CriarTurmaCommandValidator : AbstractValidator<CriarTurmaCommand>
    {
        public CriarTurmaCommandValidator()
        {
            RuleFor(x => x.EscolaId)
                .NotEmpty().WithMessage("Escola é obrigatória.");

            RuleFor(x => x.Serie)
                .NotEmpty().WithMessage("Série é obrigatória.")
                .MustBeEnum<CriarTurmaCommand, SerieEnum>().WithMessage("Série inválida.");

            RuleFor(x => x.CategoriaSerie)
                .NotEmpty().WithMessage("Categoria da série é obrigatória.")
                .MustBeEnum<CriarTurmaCommand, CategoriaSerieEnum>().WithMessage("Categoria da série inválida.");

            RuleFor(x => x.CapacidadeMaxima)
                .GreaterThan(0).WithMessage("Capacidade máxima deve ser maior que zero.")
                .LessThanOrEqualTo(1000).WithMessage("Capacidade máxima não pode ultrapassar 1000 alunos.");


            RuleFor(x => x.IdadeMinima)
                .GreaterThan(0).WithMessage("Idade mínima deve ser maior que zero.")
                .LessThanOrEqualTo(30).WithMessage("Idade mínima não pode ultrapassar 30 anos.");

            
            RuleFor(x => x.IdadeMaxima)
                .GreaterThanOrEqualTo(x => x.IdadeMinima).WithMessage("Idade máxima deve ser maior ou igual à idade mínima.")
                .LessThanOrEqualTo(30).WithMessage("Idade máxima não pode ultrapassar 30 anos.");
        }
    }
}
