using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Types;

public class AvaliacaoUlceraType : ObjectType<AvaliacaoUlcera>
{
    protected override void Configure(IObjectTypeDescriptor<AvaliacaoUlcera> descriptor)
    {
        descriptor.Name("AvaliacaoUlcera");
        descriptor.Description("Avaliação de uma úlcera venosa");

        descriptor.Field(x => x.Id).Type<IdType>().Description("ID da avaliação");
        descriptor.Field(x => x.UlceraId).Type<IdType>().Description("ID da úlcera avaliada");
        descriptor.Field(x => x.DataAvaliacao).Description("Data da avaliação");
        descriptor.Field(x => x.Duracao).Description("Duração desde o surgimento da úlcera");
        descriptor.Field(x => x.Caracteristicas).Description("Características da ferida");
        descriptor.Field(x => x.SinaisInflamatorios).Description("Sinais inflamatórios");
        descriptor.Field(x => x.Medida).Description("Medidas da ferida");
        descriptor.Field(x => x.Imagens).Description("Imagens da avaliação");
        descriptor.Field(x => x.Exsudatos).Description("Exsudatos da avaliação");
        descriptor.Field(x => x.ClassificacaoCeap).Description("Classificação CEAP");
    }
} 