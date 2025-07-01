using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Avaliacao;

public class UpdateAvaliacaoCommandHandler : IRequestHandler<UpdateAvaliacaoCommand, bool>
{
    private readonly IRepository<Domain.Entities.Avaliacao> _repository;
    public UpdateAvaliacaoCommandHandler(IRepository<Domain.Entities.Avaliacao> repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateAvaliacaoCommand request, CancellationToken cancellationToken)
    {
        var avaliacao = await _repository.GetByIdAsync(request.Id);
        if (avaliacao == null) return false;
        avaliacao.PacienteId = request.PacienteId;
        avaliacao.DataAvaliacao = request.DataAvaliacao;
        avaliacao.Observacoes = request.Observacoes;
        avaliacao.Diagnostico = request.Diagnostico;
        avaliacao.Conduta = request.Conduta;
        await _repository.UpdateAsync(avaliacao);
        return true;
    }
} 