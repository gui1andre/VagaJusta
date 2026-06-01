using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.Commands.Escolas;

namespace VagaJusta.Application.Validators
{
    public class CriarEscolaCommandValidator : AbstractValidator<Commands.Escolas.CriarEscolaCommand>
    {
        public CriarEscolaCommandValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome da escola é obrigatório.")
                .MaximumLength(200).WithMessage("Nome deve ter no máximo 200 caracteres.");

            RuleFor(x => x.Endereco)
                .NotEmpty().WithMessage("Endereço é obrigatório.")
                .MaximumLength(300).WithMessage("Endereço deve ter no máximo 300 caracteres.");
        }
    }
}
