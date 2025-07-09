// Arquivo removido: TopografiaType não é mais utilizado na nova modelagem.
// Conteúdo original comentado para build limpo.
/*
using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

public class TopografiaType : ObjectType<Topografia>
{
    protected override void Configure(IObjectTypeDescriptor<Topografia> descriptor)
    {
        descriptor.Name("Topografia");
        descriptor.Description("Topografia anatômica da úlcera");
        descriptor.Field(x => x.Id).Description("Id da topografia");
        descriptor.Field(x => x.UlceraId).Description("Id da úlcera associada");
        descriptor.Field(x => x.RegiaoId).Description("Id da região anatômica associada");
        descriptor.Field(x => x.Lado).Description("Lateralidade da topografia");
        descriptor.Field(x => x.Ulcera).Description("Úlcera associada");
        descriptor.Field(x => x.Regiao).Description("Região anatômica associada");
        // Adicione outros campos relevantes conforme necessário
    }
} 
*/