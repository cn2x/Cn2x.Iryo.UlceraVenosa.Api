using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Types;

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