using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.ImagemUlcera;

public class CreateImagemUlceraCommandHandler : IRequestHandler<CreateImagemUlceraCommand, Guid>
{
    private readonly IRepository<Domain.Entities.ImagemUlcera> _repository;
    public CreateImagemUlceraCommandHandler(IRepository<Domain.Entities.ImagemUlcera> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateImagemUlceraCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.ImagemUlcera
        {
            UlceraId = request.UlceraId,
            NomeArquivo = request.NomeArquivo,
            CaminhoArquivo = request.CaminhoArquivo,
            ContentType = request.ContentType,
            TamanhoBytes = request.TamanhoBytes,
            DataCaptura = request.DataCaptura,
            Descricao = request.Descricao,
            Observacoes = request.Observacoes,
            EhImagemPrincipal = request.EhImagemPrincipal,
            OrdemExibicao = request.OrdemExibicao,
            Desativada = false
        };
        await _repository.AddAsync(entity);
        return entity.Id;
    }
} 