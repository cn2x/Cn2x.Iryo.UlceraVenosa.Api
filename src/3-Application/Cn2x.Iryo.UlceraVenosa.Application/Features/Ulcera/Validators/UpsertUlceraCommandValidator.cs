using FluentValidation;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Commands;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Validators;

public class UpsertUlceraCommandValidator : AbstractValidator<UpsertUlceraCommand>
{
    public UpsertUlceraCommandValidator()
    {
        RuleFor(x => x.PacienteId).NotEmpty();
        // Adicione outras regras para value objects se necessário
    }
} 