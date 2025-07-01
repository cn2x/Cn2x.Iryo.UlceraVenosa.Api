using MediatR;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Cn2x.Iryo.UlceraVenosa.Domain.Specifications;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Paciente;

public class SearchPacienteQueryHandler : IRequestHandler<SearchPacienteQuery, IEnumerable<Cn2x.Iryo.UlceraVenosa.Domain.Entities.Paciente>>
{
    private readonly ApplicationDbContext _context;
    public SearchPacienteQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Domain.Entities.Paciente>> Handle(SearchPacienteQuery request, CancellationToken cancellationToken)
    {
        var spec = new PacienteFullTextSpecification(request.Term);

        return await _context.Pacientes.AsNoTracking()
            .Where(spec.SatisfiedBy())
            .ToListAsync(cancellationToken);
    }
} 