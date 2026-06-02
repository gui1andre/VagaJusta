using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VagaJusta.Application.Validators.Rules
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> MustBeEnum<T, TEnum>(
        this IRuleBuilder<T, string> ruleBuilder,
        bool ignoreCase = true)
        where TEnum : struct, Enum
        {
            return ruleBuilder
                .Must(value =>
                {
                    if (string.IsNullOrWhiteSpace(value))
                        return false;

                    return Enum.TryParse<TEnum>(
                        value,
                        ignoreCase,
                        out var parsedEnum)
                        &&
                        Enum.IsDefined(parsedEnum);
                })
                .WithMessage("{PropertyName} possui um valor inválido.");
        }
    }
}
