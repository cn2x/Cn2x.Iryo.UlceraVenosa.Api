using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Profissional.GraphQL.Types;

public class ProfissionalType : ObjectType<Domain.Entities.Profissional>
{
    protected override void Configure(IObjectTypeDescriptor<Domain.Entities.Profissional> descriptor)
    {
        descriptor.Name("Profissional");
        descriptor.Description("Profissional de saúde que realiza avaliações de úlceras");

        descriptor.Field(p => p.Id)
            .Name("id")
            .Description("Identificador único do profissional");

        descriptor.Field(p => p.Nome)
            .Name("nome")
            .Description("Nome do profissional");

        descriptor.Field(p => p.CriadoEm)
            .Name("criadoEm")
            .Description("Data de criação do registro");

        descriptor.Field(p => p.AtualizadoEm)
            .Name("atualizadoEm")
            .Description("Data da última atualização do registro");

        descriptor.Field(p => p.Avaliacoes)
            .Name("avaliacoes")
            .Description("Avaliações realizadas pelo profissional")
            .ResolveWith<ProfissionalResolvers>(r => r.GetAvaliacoes(default!, default!));
    }
}

public class ProfissionalResolvers
{
    public IEnumerable<Domain.Entities.AvaliacaoUlcera> GetAvaliacoes(Domain.Entities.Profissional profissional, [Service] IServiceProvider serviceProvider)
    {
        return profissional.Avaliacoes ?? new List<Domain.Entities.AvaliacaoUlcera>();
    }
}

