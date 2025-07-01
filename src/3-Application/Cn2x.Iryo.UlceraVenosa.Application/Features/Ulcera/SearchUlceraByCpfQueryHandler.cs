using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera;

public class SearchUlceraByCpfQueryHandler : IRequestHandler<SearchUlceraByCpfQuery, IEnumerable<Domain.Entities.Ulcera>>
{
    private readonly ApplicationDbContext _context;

    public SearchUlceraByCpfQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Domain.Entities.Ulcera>> Handle(SearchUlceraByCpfQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Cpf))
            return new List<Domain.Entities.Ulcera>();

        return await _context.Ulceras
            .Include(u => u.Caracteristicas)
            .Include(u => u.SinaisInflamatorios)
            .Include(u => u.ClassificacaoCeap)
            .Include(u => u.Topografias)
            .Include(u => u.Exsudatos)
            .Include(u => u.Imagens)
            .Include(u => u.Avaliacao)
            .Include(u => u.Paciente)
            .Where(u => !u.Desativada && u.Paciente.Cpf.Contains(request.Cpf))
            .OrderByDescending(u => u.DataExame)
            .ToListAsync(cancellationToken);
    }
} 