using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.ExsudatosDaUlcera;

public class UpsertExsudatoDaUlceraCommandHandler : IRequestHandler<UpsertExsudatoDaUlceraCommand, bool>
{
    private readonly IRepository<ExsudatoDaUlcera> _repository;

    public UpsertExsudatoDaUlceraCommandHandler(IRepository<ExsudatoDaUlcera> repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpsertExsudatoDaUlceraCommand request, CancellationToken cancellationToken)
    {
        // Verificar se já existe o vínculo
        var existing = await _repository.GetAllAsync();
        var exists = existing.Any(x => x.UlceraId == request.UlceraId && x.ExsudatoId == request.ExsudatoId);
        
        if (exists)
        {
            return true; // Já existe o vínculo
        }

        // Criar novo vínculo
        var entity = new ExsudatoDaUlcera
        {
            UlceraId = request.UlceraId,
            ExsudatoId = request.ExsudatoId
        };

        await _repository.AddAsync(entity);
        return true;
    }
} 