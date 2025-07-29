using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Types;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Medida.GraphQL.Types;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.GraphQL.Types;

public class AvaliacaoUlceraType : ObjectType<Domain.Entities.AvaliacaoUlcera>
{
    protected override void Configure(IObjectTypeDescriptor<Domain.Entities.AvaliacaoUlcera> descriptor)
    {
        descriptor.Name("AvaliacaoUlcera");
        descriptor.Description("Avaliação de uma úlcera");

        descriptor.Field(x => x.Id).Description("Id da avaliação");
        descriptor.Field(x => x.UlceraId).Description("Id da úlcera");
        descriptor.Field(x => x.DataAvaliacao).Description("Data da avaliação");
        descriptor.Field(x => x.MesesDuracao).Description("Meses de duração");
        descriptor.Field(x => x.Caracteristicas).Type<CaracteristicasType>().Description("Características da úlcera");
        descriptor.Field(x => x.SinaisInflamatorios).Type<SinaisInflamatoriosType>().Description("Sinais inflamatórios");
        descriptor.Field(x => x.Medida).Type<MedidaType>().Description("Medidas da úlcera");
        descriptor.Field(x => x.Imagens).Type<ListType<ImagemAvaliacaoUlceraType>>().Description("Imagens da avaliação");
        descriptor.Field(x => x.Exsudatos).Type<ListType<ExsudatoDaAvaliacaoType>>().Description("Exsudatos da avaliação");
    }
} 