using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Exsudato;

public class GetExsudatoByIdQuery : IRequest<Domain.Entities.Exsudato?>
{
    public Guid Id { get; set; }
    public GetExsudatoByIdQuery(Guid id) => Id = id;
} 