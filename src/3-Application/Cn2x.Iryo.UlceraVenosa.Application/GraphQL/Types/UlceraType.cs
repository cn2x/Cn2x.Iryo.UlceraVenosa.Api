using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using Cn2x.Iryo.UlceraVenosa.Domain.Models;
using Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Types;

namespace Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Types;

public class UlceraType : ObjectType<Ulcera>
{
    protected override void Configure(IObjectTypeDescriptor<Ulcera> descriptor)
    {
        descriptor.Name("Ulcera");
        descriptor.Description("Representa uma úlcera venosa");

        descriptor.Field(x => x.Id).Type<IdType>().Description("ID único da úlcera");
        descriptor.Field(x => x.PacienteId).Type<StringType>().Description("ID do paciente");
        descriptor.Field(x => x.Desativada).Description("Indica se a úlcera está desativada");
        // descriptor.Field(x => x.Topografias).Type<ListType<TopografiaType>>().Description("Topografias da úlcera");
        descriptor.Field(x => x.Paciente).Type<PacienteType>().Description("Paciente relacionado");
        descriptor.Field(x => x.Avaliacoes).Type<ListType<AvaliacaoUlceraType>>().Description("Avaliações da úlcera");
        descriptor.Field(x => x.Ceap).Type<CeapType>().Description("Classificação CEAP da úlcera");
        descriptor.Ignore(x => x.DomainEvents);
    }
}

public class CaracteristicasType : ObjectType<Caracteristicas>
{
    protected override void Configure(IObjectTypeDescriptor<Caracteristicas> descriptor)
    {
        descriptor.Name("Caracteristicas");
        descriptor.Description("Características da úlcera");

        descriptor.Field(x => x.BordasDefinidas).Description("Bordas definidas");
        descriptor.Field(x => x.TecidoGranulacao).Description("Tecido de granulação");
        descriptor.Field(x => x.Necrose).Description("Necrose");
        descriptor.Field(x => x.OdorFetido).Description("Odor fétido");
    }
}

public class SinaisInflamatoriosType : ObjectType<SinaisInflamatorios>
{
    protected override void Configure(IObjectTypeDescriptor<SinaisInflamatorios> descriptor)
    {
        descriptor.Name("SinaisInflamatorios");
        descriptor.Description("Sinais inflamatórios da úlcera");

        descriptor.Field(x => x.Dor).Description("Presença de dor");
        descriptor.Field(x => x.Calor).Description("Presença de calor");
        descriptor.Field(x => x.Rubor).Description("Presença de rubor");
        descriptor.Field(x => x.Edema).Description("Presença de edema");
    }
}

public class CeapType : ObjectType<Ceap>
{
    protected override void Configure(IObjectTypeDescriptor<Ceap> descriptor)
    {
        descriptor.Name("Ceap");
        descriptor.Description("Classificação CEAP (Clinical-Etiology-Anatomy-Pathophysiology)");

        descriptor.Field(x => x.ClasseClinica).Type<ClinicaType>().Description("Classe clínica");
        descriptor.Field(x => x.Etiologia).Type<EtiologicaType>().Description("Etiologia");
        descriptor.Field(x => x.Anatomia).Type<AnatomicaType>().Description("Anatomia");
        descriptor.Field(x => x.Patofisiologia).Type<PatofisiologicaType>().Description("Patofisiologia");
    }
}

public class ClinicaType : ObjectType<Clinica>
{
    protected override void Configure(IObjectTypeDescriptor<Clinica> descriptor)
    {
        descriptor.Name("Clinica");
        descriptor.Description("Classificação clínica da úlcera");

        descriptor.Field("id")
            .Type<EnumType<ClinicaEnum>>()
            .Resolve(ctx => ctx.Parent<Clinica>().Id);
        descriptor.Field(x => x.Name).Description("Nome da classificação clínica");
    }
}

public class EtiologicaType : ObjectType<Etiologica>
{
    protected override void Configure(IObjectTypeDescriptor<Etiologica> descriptor)
    {
        descriptor.Name("Etiologica");
        descriptor.Description("Classificação etiológica da úlcera");

        descriptor.Field("id")
            .Type<EnumType<EtiologicaEnum>>()
            .Resolve(ctx => ctx.Parent<Etiologica>().Id);
        descriptor.Field(x => x.Name).Description("Nome da classificação etiológica");
    }
}

public class AnatomicaType : ObjectType<Anatomica>
{
    protected override void Configure(IObjectTypeDescriptor<Anatomica> descriptor)
    {
        descriptor.Name("Anatomica");
        descriptor.Description("Classificação anatômica da úlcera");

        descriptor.Field("id")
            .Type<EnumType<AnatomicaEnum>>()
            .Resolve(ctx => ctx.Parent<Anatomica>().Id);
        descriptor.Field(x => x.Name).Description("Nome da classificação anatômica");
        descriptor.Field(x => x.Descricao).Description("Descrição da classificação anatômica");
    }
}

public class PatofisiologicaType : ObjectType<Patofisiologica>
{
    protected override void Configure(IObjectTypeDescriptor<Patofisiologica> descriptor)
    {
        descriptor.Name("Patofisiologica");
        descriptor.Description("Classificação patofisiológica da úlcera");

        descriptor.Field("id")
            .Type<EnumType<PatofisiologicaEnum>>()
            .Resolve(ctx => ctx.Parent<Patofisiologica>().Id);
        descriptor.Field(x => x.Name).Description("Nome da classificação patofisiológica");
    }
}

public class PagedResultUlceraType : ObjectType<PagedResult<Ulcera>>
{
    protected override void Configure(IObjectTypeDescriptor<PagedResult<Ulcera>> descriptor)
    {
        descriptor.Name("PagedResultUlcera");
        descriptor.Description("Resultado paginado de úlceras");
        descriptor.Field(x => x.Items).Type<ListType<UlceraType>>();
        descriptor.Field(x => x.TotalCount);
        descriptor.Field(x => x.Page);
        descriptor.Field(x => x.PageSize);
        descriptor.Field(x => x.TotalPages);
        descriptor.Field(x => x.HasPreviousPage);
        descriptor.Field(x => x.HasNextPage);
    }
}