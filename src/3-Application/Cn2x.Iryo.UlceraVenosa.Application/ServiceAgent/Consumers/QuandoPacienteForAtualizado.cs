
using Cn2x.Paciente.Application.Features.Paciente.Messages;
using MassTransit;
using MediatR;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Paciente;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cn2x.Iryo.UlceraVenosa.Application.Consumers 
{
    public class QuandoPacienteForAtualizado : IConsumer<PacienteUpdatedMessage> 
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<QuandoPacienteForAtualizado> _logger;

        public QuandoPacienteForAtualizado(
            IMediator mediator,
            ApplicationDbContext context,
            ILogger<QuandoPacienteForAtualizado> logger)
        {
            _mediator = mediator;
            _context = context;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<PacienteUpdatedMessage> context) 
        {
            try
            {
                var message = context.Message;
                _logger.LogInformation("Processando atualização do paciente: {PacienteId} - {Nome}", 
                    message.PacienteId, message.Nome);

                // Busca paciente existente pelo CPF
                var pacienteExistente = await _context.Pacientes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Cpf == message.Cpf);

                if (pacienteExistente is not null)
                {
                    // Atualiza paciente existente
                    var updateCommand = new UpdatePacienteCommand
                    {
                        Id = message.PacienteId,
                        Nome = message.Nome,
                        Cpf = message.Cpf
                    };

                    var resultado = await _mediator.Send(updateCommand);
                    _logger.LogInformation("Paciente atualizado com sucesso: {PacienteId}", message.PacienteId);
                }
                else
                {
                    // Cria novo paciente usando o ID do evento
                    var createCommand = new CreatePacienteCommand
                    {
                        Id = message.PacienteId,
                        Nome = message.Nome,
                        Cpf = message.Cpf
                    };

                    var novoId = await _mediator.Send(createCommand);
                    _logger.LogInformation("Novo paciente criado com sucesso: {NovoId}", novoId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar atualização do paciente: {PacienteId}", 
                    context.Message.PacienteId);
                throw;
            }
        }
    }
}
