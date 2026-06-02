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
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id do aluno é obrigatório.");

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome do aluno é obrigatório.")
                .MinimumLength(3).WithMessage("Nome deve ter pelo menos 3 caracteres.")
                .MaximumLength(150).WithMessage("Nome deve ter no máximo 150 caracteres.");

            RuleFor(x => x.DataNascimento)
                .NotEmpty().WithMessage("Data de nascimento é obrigatória.")
                .LessThan(DateTime.Today).WithMessage("Data de nascimento deve ser anterior à data de hoje.");
        }
    }
}
