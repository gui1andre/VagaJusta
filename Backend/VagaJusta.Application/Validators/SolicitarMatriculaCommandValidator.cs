using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.Commands.Matriculas;

namespace VagaJusta.Application.Validators
{
    public class SolicitarMatriculaCommandValidator : AbstractValidator<SolicitarMatriculaCommand>
    {
        public SolicitarMatriculaCommandValidator()
        {
            RuleFor(x => x.NomeAluno)
                .NotEmpty().WithMessage("Nome do aluno é obrigatório.")
                .MinimumLength(3).WithMessage("Nome deve ter pelo menos 3 caracteres.")
                .MaximumLength(150).WithMessage("Nome deve ter no máximo 150 caracteres.");

            RuleFor(x => x.CPFAluno)
                .NotEmpty().WithMessage("CPF do aluno é obrigatório.")
                .Matches(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$").WithMessage("CPF inválido.");

            RuleFor(x => x.DataNascimento)
                .NotEmpty().WithMessage("Data de nascimento é obrigatória.")
                .LessThan(DateTime.Today).WithMessage("Data de nascimento deve ser anterior à data de hoje.");

            RuleFor(x => x.TurmaId)
                .NotEmpty().WithMessage("Turma é obrigatória.");
        }
    }
}
