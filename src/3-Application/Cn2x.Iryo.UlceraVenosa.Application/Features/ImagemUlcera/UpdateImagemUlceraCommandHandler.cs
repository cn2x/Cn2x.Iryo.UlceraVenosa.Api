using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.ImagemUlcera;

public class UpdateImagemUlceraCommandHandler : IRequestHandler<UpdateImagemUlceraCommand, bool>
{
    private readonly IRepository<Domain.Entities.ImagemUlcera> _repository;
    public UpdateImagemUlceraCommandHandler(IRepository<Domain.Entities.ImagemUlcera> repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateImagemUlceraCommand request, CancellationToken cancellationToken)
    {
        var imagem = await _repository.GetByIdAsync(request.Id);
        if (imagem == null) return false;
        imagem.UlceraId = request.UlceraId;
        imagem.NomeArquivo = request.NomeArquivo;
        imagem.CaminhoArquivo = request.CaminhoArquivo;
        imagem.ContentType = request.ContentType;
        imagem.TamanhoBytes = request.TamanhoBytes;
        imagem.DataCaptura = request.DataCaptura;
        imagem.Descricao = request.Descricao;
        imagem.Observacoes = request.Observacoes;
        imagem.EhImagemPrincipal = request.EhImagemPrincipal;
        imagem.OrdemExibicao = request.OrdemExibicao;
        await _repository.UpdateAsync(imagem);
        return true;
    }
} 