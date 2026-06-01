using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.Queries.Escolas;

namespace VagaJusta.Application.Validators
{
    public class ListarEscolaQueryValidator : AbstractValidator<ListarEscolaQuery>
    {
        public ListarEscolaQueryValidator()
        {
            RuleFor(x => x.Pagina)
                .GreaterThanOrEqualTo(1).WithMessage("Página deve ser maior ou igual a 1.");
            RuleFor(x => x.TamanhoPagina)
                .InclusiveBetween(1, 50).WithMessage("Tamanho da página deve estar entre 1 e 50.");
        }
    }
}
