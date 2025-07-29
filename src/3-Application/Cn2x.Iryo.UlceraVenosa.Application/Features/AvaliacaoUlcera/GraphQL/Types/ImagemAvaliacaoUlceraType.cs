using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.GraphQL.Types;

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