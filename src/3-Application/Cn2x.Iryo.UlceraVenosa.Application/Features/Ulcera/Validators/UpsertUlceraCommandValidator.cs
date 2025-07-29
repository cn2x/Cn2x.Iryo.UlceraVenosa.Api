using FluentValidation;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Commands.Perna;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Inputs;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Commands.Pe;

public class UpsertUlceraPernaCommandValidator : AbstractValidator<UpsertUlceraPernaCommand>
{
    public UpsertUlceraPernaCommandValidator()
    {
        RuleFor(x => x.ClassificacaoCeap)
            .NotNull().WithMessage("Classificação CEAP é obrigatória.");

        When(x => x.ClassificacaoCeap != null, () =>
        {
            RuleFor(x => x.ClassificacaoCeap.ClasseClinica)
                .IsInEnum().WithMessage("Classe clínica inválida.");

            RuleFor(x => x.ClassificacaoCeap.Etiologia)
                .IsInEnum().WithMessage("Etiologia inválida.");

            RuleFor(x => x.ClassificacaoCeap.Anatomia)
                .IsInEnum().WithMessage("Anatomia inválida.");

            RuleFor(x => x.ClassificacaoCeap.Patofisiologia)
                .IsInEnum().WithMessage("Patofisiologia inválida.");
        });
    }
}

public class UpsertUlceraPeCommandValidator : AbstractValidator<UpsertUlceraPeCommand>
{
    public UpsertUlceraPeCommandValidator()
    {
        RuleFor(x => x.ClassificacaoCeap)
            .NotNull().WithMessage("Classificação CEAP é obrigatória.");

        When(x => x.ClassificacaoCeap != null, () =>
        {
            RuleFor(x => x.ClassificacaoCeap.ClasseClinica)
                .IsInEnum().WithMessage("Classe clínica inválida.");

            RuleFor(x => x.ClassificacaoCeap.Etiologia)
                .IsInEnum().WithMessage("Etiologia inválida.");

            RuleFor(x => x.ClassificacaoCeap.Anatomia)
                .IsInEnum().WithMessage("Anatomia inválida.");

            RuleFor(x => x.ClassificacaoCeap.Patofisiologia)
                .IsInEnum().WithMessage("Patofisiologia inválida.");
        });
    }
}
