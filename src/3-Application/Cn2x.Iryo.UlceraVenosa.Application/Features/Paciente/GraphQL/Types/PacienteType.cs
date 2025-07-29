using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Paciente.GraphQL.Types;

public class PacienteType : ObjectType<Domain.Entities.Paciente>
{
    protected override void Configure(IObjectTypeDescriptor<Domain.Entities.Paciente> descriptor)
    {
        descriptor.Name("Paciente");
        descriptor.Description("Paciente do sistema");
        descriptor.Field(x => x.Id).Description("Id do paciente");
        descriptor.Field(x => x.Nome).Description("Nome do paciente");
        descriptor.Field(x => x.Cpf).Description("CPF do paciente");
        descriptor.Field(x => x.Desativada).Description("Indica se o paciente está desativado");
        // Adicione outros campos relevantes conforme necessário
    }
} 