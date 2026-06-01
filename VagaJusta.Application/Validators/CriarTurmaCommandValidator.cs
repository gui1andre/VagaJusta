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
    public class CriarTurmaCommandValidator : AbstractValidator<Commands.Turmas.CriarTurmaCommand>
    {
        public CriarTurmaCommandValidator()
        {
            RuleFor(x => x.EscolaId)
                .NotEmpty().WithMessage("Escola é obrigatória.");

            RuleFor(x => x.Serie)
                .MustBeEnum<CriarTurmaCommand, SerieEnum>().WithMessage("Série inválida.");

            RuleFor(x => x.CategoriaSerie)
                .MustBeEnum<CriarTurmaCommand, CategoriaSerieEnum>().WithMessage("Categoria da série inválida.");


            RuleFor(x => x.CapacidadeMaxima)
                .GreaterThan(0).WithMessage("Capacidade máxima deve ser maior que zero.");

            RuleFor(x => x.IdadeMinima)
                .GreaterThanOrEqualTo(0).WithMessage("Idade mínima não pode ser negativa.");

            RuleFor(x => x.IdadeMaxima)
                .GreaterThan(x => x.IdadeMinima).WithMessage("Idade máxima deve ser maior que a idade mínima.");
        }
    }
}
