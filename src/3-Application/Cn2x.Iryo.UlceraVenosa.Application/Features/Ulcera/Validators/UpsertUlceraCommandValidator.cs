using FluentValidation;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Commands;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Validators;

public class UpsertUlceraCommandValidator : AbstractValidator<UpsertUlceraCommand>
{
    public UpsertUlceraCommandValidator()
    {
        RuleFor(x => x.PacienteId).NotEmpty();
        RuleFor(x => x.Duracao).NotEmpty().MaximumLength(100);
        RuleFor(x => x.DataExame).NotEqual(default(DateTime));
        RuleFor(x => x.ClasseClinica).NotNull().IsInEnum();
        RuleFor(x => x.Etiologia).NotNull().IsInEnum();
        RuleFor(x => x.Anatomia).NotNull().IsInEnum();
        RuleFor(x => x.Patofisiologia).NotNull().IsInEnum();
        RuleFor(x => x.Caracteristicas).NotNull();
        // Adicione outras regras para value objects se necessário
    }
} 