using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

public class LateralidadeType : ObjectType<Lateralidade>
{
    protected override void Configure(IObjectTypeDescriptor<Lateralidade> descriptor)
    {
        descriptor.Name("Lateralidade");
        descriptor.Description("Lateralidade da topografia");

        descriptor.Field(x => x.Id)
            .Type<IntType>()
            .Description("Id da lateralidade");
        descriptor.Field(x => x.Nome).Description("Nome da lateralidade");
    }
}

public class ImagemAvaliacaoUlceraType : ObjectType<ImagemAvaliacaoUlcera>
{
    protected override void Configure(IObjectTypeDescriptor<ImagemAvaliacaoUlcera> descriptor)
    {
        descriptor.Name("ImagemAvaliacaoUlcera");
        descriptor.Description("Imagem associada à avaliação da úlcera");
        descriptor.Field(x => x.Id).Description("Id da imagem da avaliação da úlcera");
        descriptor.Field(x => x.AvaliacaoUlceraId).Description("Id da avaliação da úlcera");
        descriptor.Field(x => x.Imagem).Description("Metadados da imagem (VO)");
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
        descriptor.Field(x => x.AvaliacaoUlceraId).Description("Id da avaliação associada");
        descriptor.Field(x => x.Comprimento).Description("Comprimento em cm");
        descriptor.Field(x => x.Largura).Description("Largura em cm");
        descriptor.Field(x => x.Profundidade).Description("Profundidade em cm");
    }
}