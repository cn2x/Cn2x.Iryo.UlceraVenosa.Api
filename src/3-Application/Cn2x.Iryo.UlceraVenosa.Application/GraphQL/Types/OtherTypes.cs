using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Types;

public class TopografiaType : ObjectType<Topografia>
{
    protected override void Configure(IObjectTypeDescriptor<Topografia> descriptor)
    {
        descriptor.Name("Topografia");
        descriptor.Description("Topografia da úlcera");

        descriptor.Field(x => x.Id).Type<IdType>().Description("ID único da topografia");
        descriptor.Field(x => x.UlceraId).Type<StringType>().Description("ID da úlcera");
        descriptor.Field(x => x.RegiaoId).Type<StringType>().Description("ID da região anatômica");
        descriptor.Field(x => x.Lado).Description("Lado da topografia");
        descriptor.Field(x => x.Desativada).Description("Indica se a topografia está desativada");
        descriptor.Ignore(x => x.DomainEvents);
    }
}

public class ExsudatoDaUlceraType : ObjectType<ExsudatoDaUlcera>
{
    protected override void Configure(IObjectTypeDescriptor<ExsudatoDaUlcera> descriptor)
    {
        descriptor.Name("ExsudatoDaUlcera");
        descriptor.Description("Exsudato da úlcera");

        descriptor.Field(x => x.Id).Type<IdType>().Description("ID único do exsudato");
        descriptor.Field(x => x.UlceraId).Type<StringType>().Description("ID da úlcera");
        descriptor.Field(x => x.ExsudatoId).Type<StringType>().Description("ID do tipo de exsudato");
        descriptor.Field(x => x.Descricao).Description("Descrição do exsudato");
        descriptor.Field(x => x.Desativada).Description("Indica se o exsudato está desativado");
        descriptor.Ignore(x => x.DomainEvents);
    }
}

public class ImagemUlceraType : ObjectType<ImagemUlcera>
{
    protected override void Configure(IObjectTypeDescriptor<ImagemUlcera> descriptor)
    {
        descriptor.Name("ImagemUlcera");
        descriptor.Description("Imagem da úlcera");

        descriptor.Field(x => x.Id).Type<IdType>().Description("ID único da imagem");
        descriptor.Field(x => x.UlceraId).Type<StringType>().Description("ID da úlcera");
        descriptor.Field(x => x.NomeArquivo).Description("Nome do arquivo");
        descriptor.Field(x => x.CaminhoArquivo).Description("Caminho do arquivo");
        descriptor.Field(x => x.ContentType).Description("Tipo de conteúdo");
        descriptor.Field(x => x.TamanhoBytes).Description("Tamanho do arquivo em bytes");
        descriptor.Field(x => x.DataCaptura).Description("Data de captura da imagem");
        descriptor.Field(x => x.Descricao).Description("Descrição da imagem");
        descriptor.Field(x => x.Observacoes).Description("Observações da imagem");
        descriptor.Field(x => x.EhImagemPrincipal).Description("Indica se é a imagem principal");
        descriptor.Field(x => x.OrdemExibicao).Description("Ordem de exibição");
        descriptor.Ignore(x => x.DomainEvents);
    }
}

public class AvaliacaoType : ObjectType<Avaliacao>
{
    protected override void Configure(IObjectTypeDescriptor<Avaliacao> descriptor)
    {
        descriptor.Name("Avaliacao");
        descriptor.Description("Avaliação médica");

        descriptor.Field(x => x.Id).Type<IdType>().Description("ID único da avaliação");
        descriptor.Field(x => x.DataAvaliacao).Description("Data da avaliação");
        descriptor.Field(x => x.Observacoes).Description("Observações da avaliação");
        descriptor.Field(x => x.Diagnostico).Description("Diagnóstico");
        descriptor.Field(x => x.Conduta).Description("Conduta médica");
        descriptor.Ignore(x => x.DomainEvents);
    }
}

public class PacienteType : ObjectType<Paciente>
{
    protected override void Configure(IObjectTypeDescriptor<Paciente> descriptor)
    {
        descriptor.Name("Paciente");
        descriptor.Description("Paciente do sistema");

        descriptor.Field(x => x.Id).Type<IdType>().Description("ID único do paciente");
        descriptor.Field(x => x.Nome).Description("Nome do paciente");
        descriptor.Field(x => x.Cpf).Description("CPF do paciente");
        descriptor.Field(x => x.Desativada).Description("Indica se o paciente está desativado");
        descriptor.Ignore(x => x.DomainEvents);
    }
}