using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

public class LateralidadeType : ObjectType<Lateralidade>
{
    protected override void Configure(IObjectTypeDescriptor<Lateralidade> descriptor)
    {
        descriptor.Name("Lateralidade");
        descriptor.Description("Lateralidade da topografia");

        descriptor.Field("id")
            .Type<EnumType<LateralidadeEnum>>()
            .Resolve(ctx => ctx.Parent<Lateralidade>().Id);
        descriptor.Field(x => x.Name).Description("Nome amigável da lateralidade");
    }
}

public class ExsudatoDaUlceraType : ObjectType<ExsudatoDaUlcera>
{
    protected override void Configure(IObjectTypeDescriptor<ExsudatoDaUlcera> descriptor)
    {
        descriptor.Name("ExsudatoDaUlcera");
        descriptor.Description("Exsudato associado à úlcera (tabela de vínculo)");
        descriptor.Field(x => x.UlceraId).Description("Id da úlcera");
        descriptor.Field(x => x.ExsudatoId).Description("Id do exsudato");
        // Adicione outros campos relevantes conforme necessário
    }
}

public class ImagemUlceraType : ObjectType<ImagemUlcera>
{
    protected override void Configure(IObjectTypeDescriptor<ImagemUlcera> descriptor)
    {
        descriptor.Name("ImagemUlcera");
        descriptor.Description("Imagem associada à úlcera");
        descriptor.Field(x => x.Id).Description("Id da imagem da úlcera");
        // Adicione outros campos relevantes conforme necessário
    }
}

public class PacienteType : ObjectType<Paciente>
{
    protected override void Configure(IObjectTypeDescriptor<Paciente> descriptor)
    {
        descriptor.Name("Paciente");
        descriptor.Description("Paciente do sistema");
        descriptor.Field(x => x.Id).Description("Id do paciente");
        // Adicione outros campos relevantes conforme necessário
    }
}

public class MedidaType : ObjectType<Medida>
{
    protected override void Configure(IObjectTypeDescriptor<Medida> descriptor)
    {
        descriptor.Name("Medida");
        descriptor.Description("Medidas da úlcera (relacionamento 1:1)");
        descriptor.Field(x => x.Id).Description("Id da Medida (igual ao UlceraId)");
        descriptor.Field(x => x.UlceraId).Description("Id da úlcera associada");
        descriptor.Field(x => x.Comprimento).Description("Comprimento em cm");
        descriptor.Field(x => x.Largura).Description("Largura em cm");
        descriptor.Field(x => x.Profundidade).Description("Profundidade em cm");
    }
} 