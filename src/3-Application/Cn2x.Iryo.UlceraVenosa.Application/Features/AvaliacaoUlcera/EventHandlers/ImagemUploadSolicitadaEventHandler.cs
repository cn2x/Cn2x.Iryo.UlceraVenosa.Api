using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Events;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Microsoft.Extensions.Logging;
using Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.Services;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.EventHandlers;

/// <summary>
/// Handler para o evento de upload de imagem solicitada
/// Responsável por fazer o upload para o Google Cloud Storage e atualizar a entidade
/// </summary>
public class ImagemUploadSolicitadaEventHandler : INotificationHandler<ImagemUploadSolicitadaEvent>
{
    private readonly ILogger<ImagemUploadSolicitadaEventHandler> _logger;
    private readonly IAvaliacaoUlceraRepository _avaliacaoUlceraRepository;
    private readonly IGoogleCloudStorageService _googleCloudStorageService;

    public ImagemUploadSolicitadaEventHandler(
        ILogger<ImagemUploadSolicitadaEventHandler> logger,
        IAvaliacaoUlceraRepository avaliacaoUlceraRepository,
        IGoogleCloudStorageService googleCloudStorageService)
    {
        _logger = logger;
        _avaliacaoUlceraRepository = avaliacaoUlceraRepository;
        _googleCloudStorageService = googleCloudStorageService;
    }

    public async Task Handle(ImagemUploadSolicitadaEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Iniciando upload de imagem para avaliação {AvaliacaoId}", notification.AvaliacaoUlceraId);

            // 1. Fazer upload para o Google Cloud Storage
            var url = await _googleCloudStorageService.UploadImagemAsync(
                notification.ArquivoBase64,
                notification.AvaliacaoUlceraId,
                cancellationToken);

            // 2. Buscar a avaliação para atualizar com a URL
            var avaliacao = await _avaliacaoUlceraRepository.GetByIdAsync(notification.AvaliacaoUlceraId);
            if (avaliacao == null)
            {
                _logger.LogError("Avaliação {AvaliacaoId} não encontrada para atualizar URL da imagem", notification.AvaliacaoUlceraId);
                return;
            }

            // 3. Encontrar a imagem correspondente e atualizar com a URL
            var imagemAvaliacao = avaliacao.Imagens.FirstOrDefault(i => 
                i.Imagem.Descricao == notification.Descricao && 
                i.Imagem.DataCaptura == notification.DataCaptura);

            if (imagemAvaliacao != null)
            {
                // Atualizar a URL da imagem
                imagemAvaliacao.Imagem.AtualizarUrl(url);
                
                // Salvar as alterações
                await _avaliacaoUlceraRepository.UpdateAsync(avaliacao);
                await _avaliacaoUlceraRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Imagem atualizada com sucesso. URL: {Url}", url);
            }
            else
            {
                _logger.LogWarning("Imagem não encontrada na avaliação {AvaliacaoId} para atualizar URL", notification.AvaliacaoUlceraId);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar upload de imagem para avaliação {AvaliacaoId}", notification.AvaliacaoUlceraId);
            throw;
        }
    }
}
