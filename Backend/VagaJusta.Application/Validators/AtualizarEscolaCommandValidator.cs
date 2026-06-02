using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using VagaJusta.Application.Commands.Escolas;

namespace VagaJusta.Application.Validators
{
    public class AtualizarEscolaCommandValidator : AbstractValidator<AtualizarEscolaCommand>
    {
        public AtualizarEscolaCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id da escola é obrigatório.");

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome da escola é obrigatório.")
                .MinimumLength(3).WithMessage("Nome deve ter pelo menos 3 caracteres.")
                .MaximumLength(200).WithMessage("Nome deve ter no máximo 200 caracteres.");

            RuleFor(x => x.Endereco)
                .NotEmpty().WithMessage("Endereço é obrigatório.")
                .MinimumLength(5).WithMessage("Endereço deve ter pelo menos 5 caracteres.")
                .MaximumLength(300).WithMessage("Endereço deve ter no máximo 200 caracteres.");
        }
    }
}
