using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.Commands.Alunos;

namespace VagaJusta.Application.Validators
{
    public class AtualizarAlunoCommandValidator : AbstractValidator<AtualizarAlunoCommand>
    {
        public AtualizarAlunoCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Nome).NotEmpty().MaximumLength(150);
            RuleFor(x => x.DataNascimento).LessThan(DateTime.Today).GreaterThan(DateTime.Today.AddYears(-18));
        }
    }
}
