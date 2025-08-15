using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.GraphQL.Inputs;
using Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.Services;
using HotChocolate.Types;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.Commands;

public class UpsertAvaliacaoUlceraCommandHandler : IRequestHandler<UpsertAvaliacaoUlceraCommand, Guid>
{
    private readonly IAvaliacaoUlceraRepository _avaliacaoUlceraRepository;
    private readonly IMediator _mediator;
    private readonly IFileUploadService _fileUploadService;

    public UpsertAvaliacaoUlceraCommandHandler(
        IAvaliacaoUlceraRepository avaliacaoUlceraRepository,
        IMediator mediator,
        IFileUploadService fileUploadService)
    {
        _avaliacaoUlceraRepository = avaliacaoUlceraRepository;
        _mediator = mediator;
        _fileUploadService = fileUploadService;
    }

    public async Task<Guid> Handle(UpsertAvaliacaoUlceraCommand request, CancellationToken cancellationToken)
    {
        Cn2x.Iryo.UlceraVenosa.Domain.Entities.AvaliacaoUlcera? avaliacao = null;
        if (request.Id != null && request.Id != Guid.Empty)
        {
            avaliacao = await _avaliacaoUlceraRepository.GetByIdAsync(request.Id.Value);
        }

        if (avaliacao is null)
        {
            // Criação
            var novaAvaliacao = new Cn2x.Iryo.UlceraVenosa.Domain.Entities.AvaliacaoUlcera
            {
                UlceraId = request.UlceraId,
                ProfissionalId = request.ProfissionalId,
                DataAvaliacao = request.DataAvaliacao,
                MesesDuracao = request.MesesDuracao,
                Caracteristicas = request.Caracteristicas,
                SinaisInflamatorios = request.SinaisInflamatorios,
                Medida = request.Medida
            };

            // Processar exsudatos se fornecidos
            if (request.Exsudatos?.Any() == true)
            {
                foreach (var exsudatoId in request.Exsudatos)
                {
                    novaAvaliacao.Exsudatos.Add(new ExsudatoDaAvaliacao
                    {
                        AvaliacaoUlceraId = novaAvaliacao.Id,
                        ExsudatoId = exsudatoId
                    });
                }
            }

            // Processar arquivo se fornecido
            if (request.Arquivo != null)
            {
                // Processar o arquivo base64 para obter bytes e metadados
                var fileResult = await _fileUploadService.ProcessBase64Async(request.Arquivo);
                
                // Criar imagem temporária (sem URL ainda)
                var imagem = new Domain.Entities.Imagem(
                    string.Empty, // URL será preenchida pelo evento de domínio
                    request.DescricaoImagem ?? fileResult.Description ?? "Imagem da úlcera",
                    request.DataCapturaImagem ?? fileResult.CaptureDate
                );
                
                novaAvaliacao.Imagens.Add(new ImagemAvaliacaoUlcera
                {
                    AvaliacaoUlceraId = novaAvaliacao.Id,
                    Imagem = imagem
                });
            }

            await _avaliacaoUlceraRepository.AddAsync(novaAvaliacao);
            await _avaliacaoUlceraRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            
            // Disparar evento para upload da imagem
            if (request.Arquivo != null)
            {
                var fileResult = await _fileUploadService.ProcessBase64Async(request.Arquivo);
                var evento = new Domain.Events.ImagemUploadSolicitadaEvent(
                    novaAvaliacao.Id,
                    request.Arquivo, // Já está em base64
                    request.DescricaoImagem ?? fileResult.Description ?? "Imagem da úlcera",
                    request.DataCapturaImagem ?? fileResult.CaptureDate
                );
                
                await _mediator.Publish(evento, cancellationToken);
            }
            
            return novaAvaliacao.Id;
        }
        else
        {
            // Atualização
            avaliacao.UlceraId = request.UlceraId;
            avaliacao.ProfissionalId = request.ProfissionalId;
            avaliacao.DataAvaliacao = request.DataAvaliacao;
            avaliacao.MesesDuracao = request.MesesDuracao;
            avaliacao.Caracteristicas = request.Caracteristicas;
            avaliacao.SinaisInflamatorios = request.SinaisInflamatorios;
            avaliacao.Medida = request.Medida;

            // Atualizar exsudatos
            await AtualizarExsudatos(avaliacao, request.Exsudatos, cancellationToken);

            // Atualizar imagens
            await AtualizarImagens(avaliacao, request.Arquivo, request.DescricaoImagem, request.DataCapturaImagem, cancellationToken);

            await _avaliacaoUlceraRepository.UpdateAsync(avaliacao);
            await _avaliacaoUlceraRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return avaliacao.Id;
        }
    }

    private async Task AtualizarExsudatos(Domain.Entities.AvaliacaoUlcera avaliacao, List<Guid>? novosExsudatos, CancellationToken cancellationToken)
    {
        // Limpar exsudatos existentes
        avaliacao.Exsudatos.Clear();

        // Adicionar novos exsudatos se fornecidos
        if (novosExsudatos?.Any() == true)
        {
            foreach (var exsudatoId in novosExsudatos)
            {
                avaliacao.Exsudatos.Add(new ExsudatoDaAvaliacao
                {
                    AvaliacaoUlceraId = avaliacao.Id,
                    ExsudatoId = exsudatoId
                });
            }
        }
    }

    private async Task AtualizarImagens(Domain.Entities.AvaliacaoUlcera avaliacao, string? novoArquivo, string? descricaoImagem, DateTime? dataCapturaImagem, CancellationToken cancellationToken)
    {
        // Só apagar imagens existentes se uma nova imagem for fornecida
        if (novoArquivo != null)
        {
            // Limpar imagens existentes (comportamento de replace)
            avaliacao.Imagens.Clear();

            // Processar o arquivo base64 para obter bytes e metadados
            var fileResult = await _fileUploadService.ProcessBase64Async(novoArquivo);
            
            // Criar imagem temporária (sem URL ainda)
            var imagem = new Domain.Entities.Imagem(
                string.Empty, // URL será preenchida pelo evento de domínio
                descricaoImagem ?? fileResult.Description ?? "Imagem da úlcera",
                dataCapturaImagem ?? fileResult.CaptureDate
            );
            
            avaliacao.Imagens.Add(new ImagemAvaliacaoUlcera
            {
                AvaliacaoUlceraId = avaliacao.Id,
                Imagem = imagem
            });

            // Disparar evento para upload da nova imagem
            var evento = new Domain.Events.ImagemUploadSolicitadaEvent(
                avaliacao.Id,
                novoArquivo, // Já está em base64
                descricaoImagem ?? fileResult.Description ?? "Imagem da úlcera",
                dataCapturaImagem ?? fileResult.CaptureDate
            );
            
            await _mediator.Publish(evento, cancellationToken);
        }
        // Se não houver novo arquivo, manter as imagens existentes (não fazer nada)
    }
}
