using HotChocolate;
using HotChocolate.Types;
using MediatR;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;
using Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Mutations;

[ExtendObjectType("Mutation")]
public class UlceraMutations
{
    public async Task<Ulcera> CreateUlceraAsync(
        CreateUlceraInput input,
        [Service] IMediator mediator)
    {
        var command = new CreateUlceraCommand
        {
            PacienteId = input.PacienteId,
            AvaliacaoId = input.AvaliacaoId,
            Duracao = input.Duracao,
            DataExame = input.DataExame,
            ComprimentoCm = input.ComprimentoCm,
            Largura = input.Largura,
            Profundidade = input.Profundidade,
            ClasseClinica = Clinica.FromValue<Clinica>((ClinicaEnum)input.ClasseClinica.Id),
            Etiologia = Etiologica.FromValue<Etiologica>((EtiologicaEnum)input.Etiologia.Id),
            Anatomia = Anatomica.FromValue<Anatomica>((AnatomicaEnum)input.Anatomia.Id),
            Patofisiologia = Patofisiologica.FromValue<Patofisiologica>((PatofisiologicaEnum)input.Patofisiologia.Id),
            Caracteristicas = input.Caracteristicas != null ? new Caracteristicas
            {
                BordasDefinidas = input.Caracteristicas.BordasDefinidas,
                TecidoGranulacao = input.Caracteristicas.TecidoGranulacao,
                Necrose = input.Caracteristicas.Necrose,
                OdorFetido = input.Caracteristicas.OdorFetido
            } : new Caracteristicas(),
            SinaisInflamatorios = input.SinaisInflamatorios != null ? new SinaisInflamatorios
            {
                Dor = input.SinaisInflamatorios.Dor,
                Calor = input.SinaisInflamatorios.Calor,
                Rubor = input.SinaisInflamatorios.Rubor,
                Edema = input.SinaisInflamatorios.Edema
            } : new SinaisInflamatorios()
        };

        var ulceraId = await mediator.Send(command);
        
        // Retornar a úlcera criada
        var query = new GetUlceraByIdQuery { Id = ulceraId };
        return await mediator.Send(query);
    }

    public async Task<Ulcera?> UpdateUlceraAsync(
        UpdateUlceraInput input,
        [Service] IMediator mediator)
    {
        var command = new UpdateUlceraCommand
        {
            Id = input.Id,
            PacienteId = input.PacienteId,
            AvaliacaoId = input.AvaliacaoId,
            Duracao = input.Duracao,
            DataExame = input.DataExame,
            ComprimentoCm = input.ComprimentoCm,
            Largura = input.Largura,
            Profundidade = input.Profundidade,
            ClasseClinica = Clinica.FromValue<Clinica>((ClinicaEnum)input.ClasseClinica.Id),
            Etiologia = Etiologica.FromValue<Etiologica>((EtiologicaEnum)input.Etiologia.Id),
            Anatomia = Anatomica.FromValue<Anatomica>((AnatomicaEnum)input.Anatomia.Id),
            Patofisiologia = Patofisiologica.FromValue<Patofisiologica>((PatofisiologicaEnum)input.Patofisiologia.Id),
            Caracteristicas = input.Caracteristicas != null ? new Caracteristicas
            {
                BordasDefinidas = input.Caracteristicas.BordasDefinidas,
                TecidoGranulacao = input.Caracteristicas.TecidoGranulacao,
                Necrose = input.Caracteristicas.Necrose,
                OdorFetido = input.Caracteristicas.OdorFetido
            } : new Caracteristicas(),
            SinaisInflamatorios = input.SinaisInflamatorios != null ? new SinaisInflamatorios
            {
                Dor = input.SinaisInflamatorios.Dor,
                Calor = input.SinaisInflamatorios.Calor,
                Rubor = input.SinaisInflamatorios.Rubor,
                Edema = input.SinaisInflamatorios.Edema
            } : new SinaisInflamatorios()
        };

        var success = await mediator.Send(command);
        
        if (success)
        {
            // Retornar a úlcera atualizada
            var query = new GetUlceraByIdQuery { Id = input.Id };
            return await mediator.Send(query);
        }
        
        return null;
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
    public Guid AvaliacaoId { get; set; }
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
    public Guid AvaliacaoId { get; set; }
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