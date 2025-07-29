using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Models;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Types;

public class PagedResultUlceraType : ObjectType<PagedResult<Domain.Entities.Ulcera>>
{
    protected override void Configure(IObjectTypeDescriptor<PagedResult<Domain.Entities.Ulcera>> descriptor)
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