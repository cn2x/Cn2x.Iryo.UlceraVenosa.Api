using HotChocolate;
using HotChocolate.Types;
using MediatR;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;
using Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Commands;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Queries;

namespace Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Mutations;

[ExtendObjectType("Mutation")]
public class UlceraMutations
{
    public async Task<Ulcera?> UpsertUlceraAsync(
        UpsertUlceraInput input,
        [Service] IMediator mediator)
    {
        var command = new UpsertUlceraCommand
        {
            Id = input.Id,
            PacienteId = input.PacienteId,
            Duracao = input.Duracao,
            DataExame = input.DataExame,
            ClasseClinica = Clinica.FromValue<Clinica>(input.ClasseClinica),
            Etiologia = Etiologica.FromValue<Etiologica>(input.Etiologia),
            Anatomia = Anatomica.FromValue<Anatomica>(input.Anatomia),
            Patofisiologia = Patofisiologica.FromValue<Patofisiologica>(input.Patofisiologia),
            Caracteristicas = input.Caracteristicas != null ? new Caracteristicas
            {
                BordasDefinidas = input.Caracteristicas.BordasDefinidas,
                TecidoGranulacao = input.Caracteristicas.TecidoGranulacao,
                Necrose = input.Caracteristicas.Necrose,
                OdorFetido = input.Caracteristicas.OdorFetido
            } : new Caracteristicas()
        };

        var ulceraId = await mediator.Send(command);
        var query = new GetUlceraByIdQuery { Id = ulceraId };
        return await mediator.Send(query);
    }

    public async Task<bool> AtivarInativarUlceraAsync(
        Guid id,
        [Service] IMediator mediator)
    {
        var command = new AtivarInativarUlceraCommand { Id = id };
        return await mediator.Send(command);
    }
}

// Classes para mapear os inputs
public class CreateUlceraInput
{
    public Guid PacienteId { get; set; }
    public string Duracao { get; set; } = string.Empty;
    public DateTime DataExame { get; set; }
    public decimal ComprimentoCm { get; set; }
    public decimal Largura { get; set; }
    public decimal Profundidade { get; set; }
    public ClinicaInput ClasseClinica { get; set; } = new();
    public EtiologicaInput Etiologia { get; set; } = new();
    public AnatomicaInput Anatomia { get; set; } = new();
    public PatofisiologicaInput Patofisiologia { get; set; } = new();
    public CaracteristicasInput? Caracteristicas { get; set; }
    public SinaisInflamatoriosInput? SinaisInflamatorios { get; set; }
}

public class UpdateUlceraInput
{
    public Guid Id { get; set; }
    public Guid PacienteId { get; set; }
    public string Duracao { get; set; } = string.Empty;
    public DateTime DataExame { get; set; }
    public decimal ComprimentoCm { get; set; }
    public decimal Largura { get; set; }
    public decimal Profundidade { get; set; }
    public ClinicaInput ClasseClinica { get; set; } = new();
    public EtiologicaInput Etiologia { get; set; } = new();
    public AnatomicaInput Anatomia { get; set; } = new();
    public PatofisiologicaInput Patofisiologia { get; set; } = new();
    public CaracteristicasInput? Caracteristicas { get; set; }
    public SinaisInflamatoriosInput? SinaisInflamatorios { get; set; }
}

public class CaracteristicasInput
{
    public bool BordasDefinidas { get; set; }
    public bool TecidoGranulacao { get; set; }
    public bool Necrose { get; set; }
    public bool OdorFetido { get; set; }
}

public class SinaisInflamatoriosInput
{
    public bool Dor { get; set; }
    public bool Calor { get; set; }
    public bool Rubor { get; set; }
    public bool Edema { get; set; }
}

public class ClinicaInput
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class EtiologicaInput
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class AnatomicaInput
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class PatofisiologicaInput
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class UpsertUlceraInput
{
    public Guid? Id { get; set; }
    public Guid PacienteId { get; set; }
    public string Duracao { get; set; } = string.Empty;
    public DateTime DataExame { get; set; }
    public ClinicaEnum ClasseClinica { get; set; }
    public EtiologicaEnum Etiologia { get; set; }
    public AnatomicaEnum Anatomia { get; set; }
    public PatofisiologicaEnum Patofisiologia { get; set; }
    public CaracteristicasInput? Caracteristicas { get; set; }
} 