using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using HotChocolate.Types.Relay;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

public class LateralidadeType : ObjectType<Lateralidade>
{
    protected override void Configure(IObjectTypeDescriptor<Lateralidade> descriptor)
    {
        descriptor.Name("Lateralidade");
        descriptor.Description("Lateralidade da topografia");

        descriptor.Field(x => x.Id)
            .Type<UuidType>()
            .Description("Id da lateralidade");
        descriptor.Field(x => x.Nome).Description("Nome da lateralidade");
    }
}

public class ImagemAvaliacaoUlceraType : ObjectType<ImagemAvaliacaoUlcera>
{
    protected override void Configure(IObjectTypeDescriptor<ImagemAvaliacaoUlcera> descriptor)
    {
        descriptor.Name("ImagemAvaliacaoUlcera");
        descriptor.Description("Imagem associada à avaliação da úlcera");
        descriptor.Field(x => x.Id).Description("Id da imagem da avaliação da úlcera");
        descriptor.Field(x => x.AvaliacaoUlceraId).Description("Id da avaliação da úlcera");
        descriptor.Field(x => x.Imagem).Description("Metadados da imagem (VO)");
    }
}

public class PacienteType : ObjectType<Paciente>
{
    protected override void Configure(IObjectTypeDescriptor<Paciente> descriptor)
    {
        descriptor.Name("Paciente");
        descriptor.Description("Paciente do sistema");
        descriptor.Field(x => x.Id).Description("Id do paciente");
        // Adicione outros campos relevantes conforme necessário
    }
}

public class MedidaType : ObjectType<Medida>
{
    protected override void Configure(IObjectTypeDescriptor<Medida> descriptor)
    {
        descriptor.Name("Medida");
        descriptor.Description("Medidas da úlcera (relacionamento 1:1)");
        descriptor.Field(x => x.Comprimento).Description("Comprimento em cm");
        descriptor.Field(x => x.Largura).Description("Largura em cm");
        descriptor.Field(x => x.Profundidade).Description("Profundidade em cm");
    }
}

public interface ITopografiaType {}

public class TopografiaInterfaceType : InterfaceType<Topografia>
{
    protected override void Configure(IInterfaceTypeDescriptor<Topografia> descriptor)
    {
        descriptor.Name("Topografia");
        descriptor.Field(x => x.Id).Description("Id da topografia");
        descriptor.ResolveAbstractType((ctx, obj) =>
        {
            if (obj is TopografiaPerna) return ctx.Schema.GetType<TopografiaPernaType>(nameof(TopografiaPerna));
            if (obj is TopografiaPe) return ctx.Schema.GetType<TopografiaPeType>(nameof(TopografiaPe));
            return null;
        });
    }
}

public class KeyValueDtoType : ObjectType<KeyValueDto>
{
    protected override void Configure(IObjectTypeDescriptor<KeyValueDto> descriptor)
    {
        descriptor.Name("KeyValueDto");
        descriptor.Field(x => x.Key).Description("Chave do par chave-valor");
        descriptor.Field(x => x.Value).Description("Valor do par chave-valor");
    }
}

public class TopografiaPernaType : ObjectType<TopografiaPerna>
{
    protected override void Configure(IObjectTypeDescriptor<TopografiaPerna> descriptor)
    {
        descriptor.Implements<TopografiaInterfaceType>();
        descriptor.Name("TopografiaPerna");
        descriptor.Description("Topografia anatômica da perna");
        descriptor.Field(x => x.Id).Description("Id da topografia");
        descriptor.Field(x => x.Lateralidade).Description("Lateralidade");
        descriptor.Field(x => x.Segmentacao).Description("Segmentação");
        descriptor.Field(x => x.RegiaoAnatomica).Description("Região anatômica");
    }
}

public class TopografiaPeType : ObjectType<TopografiaPe>
{
    protected override void Configure(IObjectTypeDescriptor<TopografiaPe> descriptor)
    {
        descriptor.Implements<TopografiaInterfaceType>();
        descriptor.Name("TopografiaPe");
        descriptor.Description("Topografia anatômica do pé");
        descriptor.Field(x => x.Id).Description("Id da topografia");
        descriptor.Field(x => x.Lateralidade).Description("Lateralidade");
        descriptor.Field(x => x.RegiaoTopograficaPe).Description("Região topográfica do pé");
    }
}