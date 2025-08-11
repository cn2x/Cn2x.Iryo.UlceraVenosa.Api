using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Profissional.Commands;

public class UpsertProfissionalCommandHandler : IRequestHandler<UpsertProfissionalCommand, Guid>
{
    private readonly IProfissionalRepository _profissionalRepository;

    public UpsertProfissionalCommandHandler(IProfissionalRepository profissionalRepository)
    {
        _profissionalRepository = profissionalRepository;
    }

    public async Task<Guid> Handle(UpsertProfissionalCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Profissional profissional;

        if (request.Id.HasValue)
        {
            // Update - busca o profissional existente
            profissional = await _profissionalRepository.GetByIdAsync(request.Id.Value);
            
            if (profissional == null)
            {
                // Se não encontrou, cria um novo
                profissional = new Domain.Entities.Profissional
                {
                    Id = request.Id.Value,
                    Nome = request.Nome
                };
                await _profissionalRepository.AddAsync(profissional);
            }
            else
            {
                // Atualiza o existente
                profissional.Nome = request.Nome;
                await _profissionalRepository.UpdateAsync(profissional);
            }
        }
        else
        {
            // Create - cria um novo profissional
            profissional = new Domain.Entities.Profissional
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome
            };
            await _profissionalRepository.AddAsync(profissional);
        }

        await _profissionalRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return profissional.Id;
    }
}

