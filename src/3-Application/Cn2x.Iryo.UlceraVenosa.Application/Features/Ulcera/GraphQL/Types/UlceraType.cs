using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Paciente.GraphQL.Types;
using Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.GraphQL.Types;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Referencia.GraphQL.Types;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Types;

public class UlceraType : ObjectType<Domain.Entities.Ulcera>
{
    protected override void Configure(IObjectTypeDescriptor<Domain.Entities.Ulcera> descriptor)
    {
        descriptor.Name("Ulcera");
        descriptor.Description("Representa uma úlcera venosa");

        descriptor.Field(x => x.Id).Type<IdType>().Description("ID único da úlcera");
        descriptor.Field(x => x.PacienteId).Type<StringType>().Description("ID do paciente");
        descriptor.Field(x => x.Desativada).Description("Indica se a úlcera está desativada");
        descriptor.Field(x => x.Paciente).Type<PacienteType>().Description("Paciente relacionado");
        descriptor.Field(x => x.Avaliacoes).Type<ListType<AvaliacaoUlceraType>>().Description("Avaliações da úlcera");
        descriptor.Field(x => x.Ceap).Type<CeapType>().Description("Classificação CEAP da úlcera");
        
        // Campo topografia usando interface para permitir tipos específicos
        descriptor.Field(x => x.Topografia).Type<TopografiaInterfaceType>().Description("Topografia da úlcera");
        
        descriptor.Ignore(x => x.DomainEvents);
    }
}

 