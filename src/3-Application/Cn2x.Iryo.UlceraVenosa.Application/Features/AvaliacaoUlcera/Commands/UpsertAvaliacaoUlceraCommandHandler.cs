using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.GraphQL.Inputs;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.Commands;

public class UpsertAvaliacaoUlceraCommandHandler : IRequestHandler<UpsertAvaliacaoUlceraCommand, Guid>
{
    private readonly IAvaliacaoUlceraRepository _avaliacaoUlceraRepository;
    private readonly IMediator _mediator;

    public UpsertAvaliacaoUlceraCommandHandler(
        IAvaliacaoUlceraRepository avaliacaoUlceraRepository,
        IMediator mediator)
    {
        _avaliacaoUlceraRepository = avaliacaoUlceraRepository;
        _mediator = mediator;
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

            // Processar imagens se fornecidas
            if (request.Imagens?.Any() == true)
            {
                foreach (var imagemInput in request.Imagens)
                {
                    // Criar imagem temporária (sem URL ainda)
                    var imagem = new Domain.Entities.Imagem(
                        string.Empty, // URL será preenchida pelo evento de domínio
                        imagemInput.Descricao,
                        imagemInput.DataCaptura
                    );
                    
                    novaAvaliacao.Imagens.Add(new ImagemAvaliacaoUlcera
                    {
                        AvaliacaoUlceraId = novaAvaliacao.Id,
                        Imagem = imagem
                    });
                }
            }

            await _avaliacaoUlceraRepository.AddAsync(novaAvaliacao);
            await _avaliacaoUlceraRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            
            // Disparar eventos para upload das imagens
            if (request.Imagens?.Any() == true)
            {
                foreach (var imagemInput in request.Imagens)
                {
                    var evento = new Domain.Events.ImagemUploadSolicitadaEvent(
                        novaAvaliacao.Id,
                        imagemInput.ArquivoBase64,
                        imagemInput.Descricao,
                        imagemInput.DataCaptura
                    );
                    
                    await _mediator.Publish(evento, cancellationToken);
                }
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
            await AtualizarImagens(avaliacao, request.Imagens, cancellationToken);

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

    private async Task AtualizarImagens(Domain.Entities.AvaliacaoUlcera avaliacao, List<ImagemInput>? novasImagens, CancellationToken cancellationToken)
    {
        // Limpar imagens existentes
        avaliacao.Imagens.Clear();

        // Adicionar novas imagens se fornecidas
        if (novasImagens?.Any() == true)
        {
            foreach (var imagemInput in novasImagens)
            {
                // Criar imagem temporária (sem URL ainda)
                var imagem = new Domain.Entities.Imagem(
                    string.Empty, // URL será preenchida pelo evento de domínio
                    imagemInput.Descricao,
                    imagemInput.DataCaptura
                );
                
                avaliacao.Imagens.Add(new ImagemAvaliacaoUlcera
                {
                    AvaliacaoUlceraId = avaliacao.Id,
                    Imagem = imagem
                });
            }
        }
    }
}
