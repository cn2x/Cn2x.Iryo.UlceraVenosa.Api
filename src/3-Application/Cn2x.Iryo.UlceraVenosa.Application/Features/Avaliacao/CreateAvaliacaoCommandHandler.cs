using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Avaliacao;

public class CreateAvaliacaoCommandHandler : IRequestHandler<CreateAvaliacaoCommand, Guid>
{
    private readonly IRepository<Domain.Entities.Avaliacao> _repository;
    public CreateAvaliacaoCommandHandler(IRepository<Domain.Entities.Avaliacao> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateAvaliacaoCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Avaliacao
        {
            PacienteId = request.PacienteId,
            DataAvaliacao = request.DataAvaliacao,
            Observacoes = request.Observacoes,
            Diagnostico = request.Diagnostico,
            Conduta = request.Conduta,
            Desativada = false
        };
        await _repository.AddAsync(entity);
        return entity.Id;
    }
} 