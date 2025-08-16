using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Paciente;

public class UpsertPacienteCommandRequest : IRequest<bool>
{
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
}