using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAtivoFromPaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Pacientes_PacienteId",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Exsudatos_ExsudatoTipos_ExsudatoId",
                table: "Exsudatos");

            migrationBuilder.DropForeignKey(
                name: "FK_Exsudatos_Ulceras_UlceraId",
                table: "Exsudatos");

            migrationBuilder.DropForeignKey(
                name: "FK_ImagemUlcera_Ulceras_UlceraId",
                table: "ImagemUlcera");

            migrationBuilder.DropForeignKey(
                name: "FK_RegioesAnatomicas_Segmentos_SegmentoId",
                table: "RegioesAnatomicas");

            migrationBuilder.DropForeignKey(
                name: "FK_Topografias_RegioesAnatomicas_RegiaoId",
                table: "Topografias");

            migrationBuilder.DropForeignKey(
                name: "FK_Topografias_Ulceras_UlceraId",
                table: "Topografias");

            migrationBuilder.DropForeignKey(
                name: "FK_Ulceras_Avaliacoes_AvaliacaoId",
                table: "Ulceras");

            migrationBuilder.DropForeignKey(
                name: "FK_Ulceras_Pacientes_PacienteId",
                table: "Ulceras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ulceras",
                table: "Ulceras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topografias",
                table: "Topografias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Segmentos",
                table: "Segmentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pacientes",
                table: "Pacientes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exsudatos",
                table: "Exsudatos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Avaliacoes",
                table: "Avaliacoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegioesAnatomicas",
                table: "RegioesAnatomicas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImagemUlcera",
                table: "ImagemUlcera");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExsudatoTipos",
                table: "ExsudatoTipos");

            migrationBuilder.DropColumn(
                name: "BordasDefinidas",
                table: "Ulceras");

            migrationBuilder.DropColumn(
                name: "Calor",
                table: "Ulceras");

            migrationBuilder.DropColumn(
                name: "ClassificacaoCeap_Anatomia",
                table: "Ulceras");

            migrationBuilder.DropColumn(
                name: "ClassificacaoCeap_ClasseClinica",
                table: "Ulceras");

            migrationBuilder.DropColumn(
                name: "ClassificacaoCeap_Etiologia",
                table: "Ulceras");

            migrationBuilder.DropColumn(
                name: "ClassificacaoCeap_Patofisiologia",
                table: "Ulceras");

            migrationBuilder.DropColumn(
                name: "Dor",
                table: "Ulceras");

            migrationBuilder.DropColumn(
                name: "Edema",
                table: "Ulceras");

            migrationBuilder.DropColumn(
                name: "Eritema",
                table: "Ulceras");

            migrationBuilder.DropColumn(
                name: "Necrose",
                table: "Ulceras");

            migrationBuilder.DropColumn(
                name: "OdorFetido",
                table: "Ulceras");

            migrationBuilder.DropColumn(
                name: "PerdadeFuncao",
                table: "Ulceras");

            migrationBuilder.DropColumn(
                name: "Rubor",
                table: "Ulceras");

            migrationBuilder.DropColumn(
                name: "TecidoGranulacao",
                table: "Ulceras");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Pacientes");

            migrationBuilder.RenameTable(
                name: "Ulceras",
                newName: "ulceras");

            migrationBuilder.RenameTable(
                name: "Topografias",
                newName: "topografias");

            migrationBuilder.RenameTable(
                name: "Segmentos",
                newName: "segmentos");

            migrationBuilder.RenameTable(
                name: "Pacientes",
                newName: "pacientes");

            migrationBuilder.RenameTable(
                name: "Exsudatos",
                newName: "exsudatos");

            migrationBuilder.RenameTable(
                name: "Avaliacoes",
                newName: "avaliacoes");

            migrationBuilder.RenameTable(
                name: "RegioesAnatomicas",
                newName: "regioes_anatomicas");

            migrationBuilder.RenameTable(
                name: "ImagemUlcera",
                newName: "imagem_ulcera");

            migrationBuilder.RenameTable(
                name: "ExsudatoTipos",
                newName: "exsudato_tipos");

            migrationBuilder.RenameColumn(
                name: "Profundidade",
                table: "ulceras",
                newName: "profundidade");

            migrationBuilder.RenameColumn(
                name: "Largura",
                table: "ulceras",
                newName: "largura");

            migrationBuilder.RenameColumn(
                name: "Duracao",
                table: "ulceras",
                newName: "duracao");

            migrationBuilder.RenameColumn(
                name: "Desativada",
                table: "ulceras",
                newName: "desativada");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ulceras",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PacienteId",
                table: "ulceras",
                newName: "paciente_id");

            migrationBuilder.RenameColumn(
                name: "DataExame",
                table: "ulceras",
                newName: "data_exame");

            migrationBuilder.RenameColumn(
                name: "CriadoEm",
                table: "ulceras",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "ComprimentoCm",
                table: "ulceras",
                newName: "comprimento_cm");

            migrationBuilder.RenameColumn(
                name: "AvaliacaoId",
                table: "ulceras",
                newName: "avaliacao_id");

            migrationBuilder.RenameColumn(
                name: "AtualizadoEm",
                table: "ulceras",
                newName: "atualizado_em");

            migrationBuilder.RenameIndex(
                name: "IX_Ulceras_PacienteId",
                table: "ulceras",
                newName: "ix_ulceras_paciente_id");

            migrationBuilder.RenameIndex(
                name: "IX_Ulceras_AvaliacaoId",
                table: "ulceras",
                newName: "ix_ulceras_avaliacao_id");

            migrationBuilder.RenameColumn(
                name: "Desativada",
                table: "topografias",
                newName: "desativada");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "topografias",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UlceraId",
                table: "topografias",
                newName: "ulcera_id");

            migrationBuilder.RenameColumn(
                name: "RegiaoId",
                table: "topografias",
                newName: "regiao_id");

            migrationBuilder.RenameColumn(
                name: "CriadoEm",
                table: "topografias",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "AtualizadoEm",
                table: "topografias",
                newName: "atualizado_em");

            migrationBuilder.RenameIndex(
                name: "IX_Topografias_UlceraId_RegiaoId_Lado",
                table: "topografias",
                newName: "IX_topografias_ulcera_id_regiao_id_Lado");

            migrationBuilder.RenameIndex(
                name: "IX_Topografias_RegiaoId",
                table: "topografias",
                newName: "ix_topografias_regiao_id");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "segmentos",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "segmentos",
                newName: "descricao");

            migrationBuilder.RenameColumn(
                name: "Desativada",
                table: "segmentos",
                newName: "desativada");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "segmentos",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "CriadoEm",
                table: "segmentos",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "AtualizadoEm",
                table: "segmentos",
                newName: "atualizado_em");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "pacientes",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Desativada",
                table: "pacientes",
                newName: "desativada");

            migrationBuilder.RenameColumn(
                name: "Cpf",
                table: "pacientes",
                newName: "cpf");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "pacientes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "CriadoEm",
                table: "pacientes",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "AtualizadoEm",
                table: "pacientes",
                newName: "atualizado_em");

            migrationBuilder.RenameIndex(
                name: "IX_Pacientes_Cpf",
                table: "pacientes",
                newName: "IX_pacientes_cpf");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "exsudatos",
                newName: "descricao");

            migrationBuilder.RenameColumn(
                name: "Desativada",
                table: "exsudatos",
                newName: "desativada");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "exsudatos",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UlceraId",
                table: "exsudatos",
                newName: "ulcera_id");

            migrationBuilder.RenameColumn(
                name: "ExsudatoId",
                table: "exsudatos",
                newName: "exsudato_id");

            migrationBuilder.RenameColumn(
                name: "CriadoEm",
                table: "exsudatos",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "AtualizadoEm",
                table: "exsudatos",
                newName: "atualizado_em");

            migrationBuilder.RenameIndex(
                name: "IX_Exsudatos_UlceraId",
                table: "exsudatos",
                newName: "ix_exsudatos_ulcera_id");

            migrationBuilder.RenameIndex(
                name: "IX_Exsudatos_ExsudatoId",
                table: "exsudatos",
                newName: "ix_exsudatos_exsudato_id");

            migrationBuilder.RenameColumn(
                name: "Observacoes",
                table: "avaliacoes",
                newName: "observacoes");

            migrationBuilder.RenameColumn(
                name: "Diagnostico",
                table: "avaliacoes",
                newName: "diagnostico");

            migrationBuilder.RenameColumn(
                name: "Desativada",
                table: "avaliacoes",
                newName: "desativada");

            migrationBuilder.RenameColumn(
                name: "Conduta",
                table: "avaliacoes",
                newName: "conduta");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "avaliacoes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PacienteId",
                table: "avaliacoes",
                newName: "paciente_id");

            migrationBuilder.RenameColumn(
                name: "DataAvaliacao",
                table: "avaliacoes",
                newName: "data_avaliacao");

            migrationBuilder.RenameColumn(
                name: "CriadoEm",
                table: "avaliacoes",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "AtualizadoEm",
                table: "avaliacoes",
                newName: "atualizado_em");

            migrationBuilder.RenameIndex(
                name: "IX_Avaliacoes_PacienteId",
                table: "avaliacoes",
                newName: "ix_avaliacoes_paciente_id");

            migrationBuilder.RenameColumn(
                name: "Limites",
                table: "regioes_anatomicas",
                newName: "limites");

            migrationBuilder.RenameColumn(
                name: "Desativada",
                table: "regioes_anatomicas",
                newName: "desativada");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "regioes_anatomicas",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "SegmentoId",
                table: "regioes_anatomicas",
                newName: "segmento_id");

            migrationBuilder.RenameColumn(
                name: "CriadoEm",
                table: "regioes_anatomicas",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "AtualizadoEm",
                table: "regioes_anatomicas",
                newName: "atualizado_em");

            migrationBuilder.RenameIndex(
                name: "IX_RegioesAnatomicas_SegmentoId",
                table: "regioes_anatomicas",
                newName: "ix_regioes_anatomicas_segmento_id");

            migrationBuilder.RenameColumn(
                name: "Observacoes",
                table: "imagem_ulcera",
                newName: "observacoes");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "imagem_ulcera",
                newName: "descricao");

            migrationBuilder.RenameColumn(
                name: "Desativada",
                table: "imagem_ulcera",
                newName: "desativada");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "imagem_ulcera",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UlceraId",
                table: "imagem_ulcera",
                newName: "ulcera_id");

            migrationBuilder.RenameColumn(
                name: "TamanhoBytes",
                table: "imagem_ulcera",
                newName: "tamanho_bytes");

            migrationBuilder.RenameColumn(
                name: "OrdemExibicao",
                table: "imagem_ulcera",
                newName: "ordem_exibicao");

            migrationBuilder.RenameColumn(
                name: "NomeArquivo",
                table: "imagem_ulcera",
                newName: "nome_arquivo");

            migrationBuilder.RenameColumn(
                name: "EhImagemPrincipal",
                table: "imagem_ulcera",
                newName: "eh_imagem_principal");

            migrationBuilder.RenameColumn(
                name: "DataCaptura",
                table: "imagem_ulcera",
                newName: "data_captura");

            migrationBuilder.RenameColumn(
                name: "CriadoEm",
                table: "imagem_ulcera",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "ContentType",
                table: "imagem_ulcera",
                newName: "content_type");

            migrationBuilder.RenameColumn(
                name: "CaminhoArquivo",
                table: "imagem_ulcera",
                newName: "caminho_arquivo");

            migrationBuilder.RenameColumn(
                name: "AtualizadoEm",
                table: "imagem_ulcera",
                newName: "atualizado_em");

            migrationBuilder.RenameIndex(
                name: "IX_ImagemUlcera_UlceraId",
                table: "imagem_ulcera",
                newName: "ix_imagem_ulcera_ulcera_id");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "exsudato_tipos",
                newName: "descricao");

            migrationBuilder.RenameColumn(
                name: "Desativada",
                table: "exsudato_tipos",
                newName: "desativada");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "exsudato_tipos",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "CriadoEm",
                table: "exsudato_tipos",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "AtualizadoEm",
                table: "exsudato_tipos",
                newName: "atualizado_em");

            migrationBuilder.AddPrimaryKey(
                name: "pk_ulceras",
                table: "ulceras",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_topografias",
                table: "topografias",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_segmentos",
                table: "segmentos",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_pacientes",
                table: "pacientes",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_exsudatos",
                table: "exsudatos",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_avaliacoes",
                table: "avaliacoes",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_regioes_anatomicas",
                table: "regioes_anatomicas",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_imagem_ulcera",
                table: "imagem_ulcera",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_exsudato_tipos",
                table: "exsudato_tipos",
                column: "id");

            migrationBuilder.CreateTable(
                name: "caracteristicas",
                columns: table => new
                {
                    UlceraId = table.Column<Guid>(type: "uuid", nullable: false),
                    BordasDefinidas = table.Column<bool>(type: "boolean", nullable: false),
                    TecidoGranulacao = table.Column<bool>(type: "boolean", nullable: false),
                    Necrose = table.Column<bool>(type: "boolean", nullable: false),
                    OdorFetido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_caracteristicas", x => x.UlceraId);
                    table.ForeignKey(
                        name: "FK_caracteristicas_ulceras_UlceraId",
                        column: x => x.UlceraId,
                        principalTable: "ulceras",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ceap",
                columns: table => new
                {
                    UlceraId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClasseClinica = table.Column<int>(type: "integer", nullable: false),
                    Etiologia = table.Column<int>(type: "integer", nullable: false),
                    Anatomia = table.Column<int>(type: "integer", nullable: false),
                    Patofisiologia = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ceap", x => x.UlceraId);
                    table.ForeignKey(
                        name: "FK_ceap_ulceras_UlceraId",
                        column: x => x.UlceraId,
                        principalTable: "ulceras",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sinais_inflamatorios",
                columns: table => new
                {
                    UlceraId = table.Column<Guid>(type: "uuid", nullable: false),
                    Eritema = table.Column<bool>(type: "boolean", nullable: false),
                    Calor = table.Column<bool>(type: "boolean", nullable: false),
                    Rubor = table.Column<bool>(type: "boolean", nullable: false),
                    Edema = table.Column<bool>(type: "boolean", nullable: false),
                    Dor = table.Column<bool>(type: "boolean", nullable: false),
                    PerdadeFuncao = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sinais_inflamatorios", x => x.UlceraId);
                    table.ForeignKey(
                        name: "FK_sinais_inflamatorios_ulceras_UlceraId",
                        column: x => x.UlceraId,
                        principalTable: "ulceras",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_avaliacoes_pacientes_paciente_id",
                table: "avaliacoes",
                column: "paciente_id",
                principalTable: "pacientes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_exsudatos_exsudato_tipos_exsudato_id",
                table: "exsudatos",
                column: "exsudato_id",
                principalTable: "exsudato_tipos",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_exsudatos_ulceras_ulcera_id",
                table: "exsudatos",
                column: "ulcera_id",
                principalTable: "ulceras",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_imagem_ulcera_ulceras_ulcera_id",
                table: "imagem_ulcera",
                column: "ulcera_id",
                principalTable: "ulceras",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_regioes_anatomicas_segmentos_segmento_id",
                table: "regioes_anatomicas",
                column: "segmento_id",
                principalTable: "segmentos",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_topografias_regioes_anatomicas_regiao_id",
                table: "topografias",
                column: "regiao_id",
                principalTable: "regioes_anatomicas",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_topografias_ulceras_ulcera_id",
                table: "topografias",
                column: "ulcera_id",
                principalTable: "ulceras",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ulceras_avaliacoes_avaliacao_id",
                table: "ulceras",
                column: "avaliacao_id",
                principalTable: "avaliacoes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ulceras_pacientes_paciente_id",
                table: "ulceras",
                column: "paciente_id",
                principalTable: "pacientes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_avaliacoes_pacientes_paciente_id",
                table: "avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_exsudatos_exsudato_tipos_exsudato_id",
                table: "exsudatos");

            migrationBuilder.DropForeignKey(
                name: "FK_exsudatos_ulceras_ulcera_id",
                table: "exsudatos");

            migrationBuilder.DropForeignKey(
                name: "FK_imagem_ulcera_ulceras_ulcera_id",
                table: "imagem_ulcera");

            migrationBuilder.DropForeignKey(
                name: "FK_regioes_anatomicas_segmentos_segmento_id",
                table: "regioes_anatomicas");

            migrationBuilder.DropForeignKey(
                name: "FK_topografias_regioes_anatomicas_regiao_id",
                table: "topografias");

            migrationBuilder.DropForeignKey(
                name: "FK_topografias_ulceras_ulcera_id",
                table: "topografias");

            migrationBuilder.DropForeignKey(
                name: "FK_ulceras_avaliacoes_avaliacao_id",
                table: "ulceras");

            migrationBuilder.DropForeignKey(
                name: "FK_ulceras_pacientes_paciente_id",
                table: "ulceras");

            migrationBuilder.DropTable(
                name: "caracteristicas");

            migrationBuilder.DropTable(
                name: "ceap");

            migrationBuilder.DropTable(
                name: "sinais_inflamatorios");

            migrationBuilder.DropPrimaryKey(
                name: "pk_ulceras",
                table: "ulceras");

            migrationBuilder.DropPrimaryKey(
                name: "pk_topografias",
                table: "topografias");

            migrationBuilder.DropPrimaryKey(
                name: "pk_segmentos",
                table: "segmentos");

            migrationBuilder.DropPrimaryKey(
                name: "pk_pacientes",
                table: "pacientes");

            migrationBuilder.DropPrimaryKey(
                name: "pk_exsudatos",
                table: "exsudatos");

            migrationBuilder.DropPrimaryKey(
                name: "pk_avaliacoes",
                table: "avaliacoes");

            migrationBuilder.DropPrimaryKey(
                name: "pk_regioes_anatomicas",
                table: "regioes_anatomicas");

            migrationBuilder.DropPrimaryKey(
                name: "pk_imagem_ulcera",
                table: "imagem_ulcera");

            migrationBuilder.DropPrimaryKey(
                name: "pk_exsudato_tipos",
                table: "exsudato_tipos");

            migrationBuilder.RenameTable(
                name: "ulceras",
                newName: "Ulceras");

            migrationBuilder.RenameTable(
                name: "topografias",
                newName: "Topografias");

            migrationBuilder.RenameTable(
                name: "segmentos",
                newName: "Segmentos");

            migrationBuilder.RenameTable(
                name: "pacientes",
                newName: "Pacientes");

            migrationBuilder.RenameTable(
                name: "exsudatos",
                newName: "Exsudatos");

            migrationBuilder.RenameTable(
                name: "avaliacoes",
                newName: "Avaliacoes");

            migrationBuilder.RenameTable(
                name: "regioes_anatomicas",
                newName: "RegioesAnatomicas");

            migrationBuilder.RenameTable(
                name: "imagem_ulcera",
                newName: "ImagemUlcera");

            migrationBuilder.RenameTable(
                name: "exsudato_tipos",
                newName: "ExsudatoTipos");

            migrationBuilder.RenameColumn(
                name: "profundidade",
                table: "Ulceras",
                newName: "Profundidade");

            migrationBuilder.RenameColumn(
                name: "largura",
                table: "Ulceras",
                newName: "Largura");

            migrationBuilder.RenameColumn(
                name: "duracao",
                table: "Ulceras",
                newName: "Duracao");

            migrationBuilder.RenameColumn(
                name: "desativada",
                table: "Ulceras",
                newName: "Desativada");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Ulceras",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "paciente_id",
                table: "Ulceras",
                newName: "PacienteId");

            migrationBuilder.RenameColumn(
                name: "data_exame",
                table: "Ulceras",
                newName: "DataExame");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "Ulceras",
                newName: "CriadoEm");

            migrationBuilder.RenameColumn(
                name: "comprimento_cm",
                table: "Ulceras",
                newName: "ComprimentoCm");

            migrationBuilder.RenameColumn(
                name: "avaliacao_id",
                table: "Ulceras",
                newName: "AvaliacaoId");

            migrationBuilder.RenameColumn(
                name: "atualizado_em",
                table: "Ulceras",
                newName: "AtualizadoEm");

            migrationBuilder.RenameIndex(
                name: "ix_ulceras_paciente_id",
                table: "Ulceras",
                newName: "IX_Ulceras_PacienteId");

            migrationBuilder.RenameIndex(
                name: "ix_ulceras_avaliacao_id",
                table: "Ulceras",
                newName: "IX_Ulceras_AvaliacaoId");

            migrationBuilder.RenameColumn(
                name: "desativada",
                table: "Topografias",
                newName: "Desativada");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Topografias",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ulcera_id",
                table: "Topografias",
                newName: "UlceraId");

            migrationBuilder.RenameColumn(
                name: "regiao_id",
                table: "Topografias",
                newName: "RegiaoId");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "Topografias",
                newName: "CriadoEm");

            migrationBuilder.RenameColumn(
                name: "atualizado_em",
                table: "Topografias",
                newName: "AtualizadoEm");

            migrationBuilder.RenameIndex(
                name: "IX_topografias_ulcera_id_regiao_id_Lado",
                table: "Topografias",
                newName: "IX_Topografias_UlceraId_RegiaoId_Lado");

            migrationBuilder.RenameIndex(
                name: "ix_topografias_regiao_id",
                table: "Topografias",
                newName: "IX_Topografias_RegiaoId");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Segmentos",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "descricao",
                table: "Segmentos",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "desativada",
                table: "Segmentos",
                newName: "Desativada");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Segmentos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "Segmentos",
                newName: "CriadoEm");

            migrationBuilder.RenameColumn(
                name: "atualizado_em",
                table: "Segmentos",
                newName: "AtualizadoEm");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Pacientes",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "desativada",
                table: "Pacientes",
                newName: "Desativada");

            migrationBuilder.RenameColumn(
                name: "cpf",
                table: "Pacientes",
                newName: "Cpf");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Pacientes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "Pacientes",
                newName: "CriadoEm");

            migrationBuilder.RenameColumn(
                name: "atualizado_em",
                table: "Pacientes",
                newName: "AtualizadoEm");

            migrationBuilder.RenameIndex(
                name: "IX_pacientes_cpf",
                table: "Pacientes",
                newName: "IX_Pacientes_Cpf");

            migrationBuilder.RenameColumn(
                name: "descricao",
                table: "Exsudatos",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "desativada",
                table: "Exsudatos",
                newName: "Desativada");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Exsudatos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ulcera_id",
                table: "Exsudatos",
                newName: "UlceraId");

            migrationBuilder.RenameColumn(
                name: "exsudato_id",
                table: "Exsudatos",
                newName: "ExsudatoId");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "Exsudatos",
                newName: "CriadoEm");

            migrationBuilder.RenameColumn(
                name: "atualizado_em",
                table: "Exsudatos",
                newName: "AtualizadoEm");

            migrationBuilder.RenameIndex(
                name: "ix_exsudatos_ulcera_id",
                table: "Exsudatos",
                newName: "IX_Exsudatos_UlceraId");

            migrationBuilder.RenameIndex(
                name: "ix_exsudatos_exsudato_id",
                table: "Exsudatos",
                newName: "IX_Exsudatos_ExsudatoId");

            migrationBuilder.RenameColumn(
                name: "observacoes",
                table: "Avaliacoes",
                newName: "Observacoes");

            migrationBuilder.RenameColumn(
                name: "diagnostico",
                table: "Avaliacoes",
                newName: "Diagnostico");

            migrationBuilder.RenameColumn(
                name: "desativada",
                table: "Avaliacoes",
                newName: "Desativada");

            migrationBuilder.RenameColumn(
                name: "conduta",
                table: "Avaliacoes",
                newName: "Conduta");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Avaliacoes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "paciente_id",
                table: "Avaliacoes",
                newName: "PacienteId");

            migrationBuilder.RenameColumn(
                name: "data_avaliacao",
                table: "Avaliacoes",
                newName: "DataAvaliacao");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "Avaliacoes",
                newName: "CriadoEm");

            migrationBuilder.RenameColumn(
                name: "atualizado_em",
                table: "Avaliacoes",
                newName: "AtualizadoEm");

            migrationBuilder.RenameIndex(
                name: "ix_avaliacoes_paciente_id",
                table: "Avaliacoes",
                newName: "IX_Avaliacoes_PacienteId");

            migrationBuilder.RenameColumn(
                name: "limites",
                table: "RegioesAnatomicas",
                newName: "Limites");

            migrationBuilder.RenameColumn(
                name: "desativada",
                table: "RegioesAnatomicas",
                newName: "Desativada");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "RegioesAnatomicas",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "segmento_id",
                table: "RegioesAnatomicas",
                newName: "SegmentoId");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "RegioesAnatomicas",
                newName: "CriadoEm");

            migrationBuilder.RenameColumn(
                name: "atualizado_em",
                table: "RegioesAnatomicas",
                newName: "AtualizadoEm");

            migrationBuilder.RenameIndex(
                name: "ix_regioes_anatomicas_segmento_id",
                table: "RegioesAnatomicas",
                newName: "IX_RegioesAnatomicas_SegmentoId");

            migrationBuilder.RenameColumn(
                name: "observacoes",
                table: "ImagemUlcera",
                newName: "Observacoes");

            migrationBuilder.RenameColumn(
                name: "descricao",
                table: "ImagemUlcera",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "desativada",
                table: "ImagemUlcera",
                newName: "Desativada");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ImagemUlcera",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ulcera_id",
                table: "ImagemUlcera",
                newName: "UlceraId");

            migrationBuilder.RenameColumn(
                name: "tamanho_bytes",
                table: "ImagemUlcera",
                newName: "TamanhoBytes");

            migrationBuilder.RenameColumn(
                name: "ordem_exibicao",
                table: "ImagemUlcera",
                newName: "OrdemExibicao");

            migrationBuilder.RenameColumn(
                name: "nome_arquivo",
                table: "ImagemUlcera",
                newName: "NomeArquivo");

            migrationBuilder.RenameColumn(
                name: "eh_imagem_principal",
                table: "ImagemUlcera",
                newName: "EhImagemPrincipal");

            migrationBuilder.RenameColumn(
                name: "data_captura",
                table: "ImagemUlcera",
                newName: "DataCaptura");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "ImagemUlcera",
                newName: "CriadoEm");

            migrationBuilder.RenameColumn(
                name: "content_type",
                table: "ImagemUlcera",
                newName: "ContentType");

            migrationBuilder.RenameColumn(
                name: "caminho_arquivo",
                table: "ImagemUlcera",
                newName: "CaminhoArquivo");

            migrationBuilder.RenameColumn(
                name: "atualizado_em",
                table: "ImagemUlcera",
                newName: "AtualizadoEm");

            migrationBuilder.RenameIndex(
                name: "ix_imagem_ulcera_ulcera_id",
                table: "ImagemUlcera",
                newName: "IX_ImagemUlcera_UlceraId");

            migrationBuilder.RenameColumn(
                name: "descricao",
                table: "ExsudatoTipos",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "desativada",
                table: "ExsudatoTipos",
                newName: "Desativada");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ExsudatoTipos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "ExsudatoTipos",
                newName: "CriadoEm");

            migrationBuilder.RenameColumn(
                name: "atualizado_em",
                table: "ExsudatoTipos",
                newName: "AtualizadoEm");

            migrationBuilder.AddColumn<bool>(
                name: "BordasDefinidas",
                table: "Ulceras",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Calor",
                table: "Ulceras",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ClassificacaoCeap_Anatomia",
                table: "Ulceras",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClassificacaoCeap_ClasseClinica",
                table: "Ulceras",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClassificacaoCeap_Etiologia",
                table: "Ulceras",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClassificacaoCeap_Patofisiologia",
                table: "Ulceras",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Dor",
                table: "Ulceras",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Edema",
                table: "Ulceras",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eritema",
                table: "Ulceras",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Necrose",
                table: "Ulceras",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OdorFetido",
                table: "Ulceras",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PerdadeFuncao",
                table: "Ulceras",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Rubor",
                table: "Ulceras",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TecidoGranulacao",
                table: "Ulceras",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Pacientes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ulceras",
                table: "Ulceras",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topografias",
                table: "Topografias",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Segmentos",
                table: "Segmentos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pacientes",
                table: "Pacientes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exsudatos",
                table: "Exsudatos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Avaliacoes",
                table: "Avaliacoes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegioesAnatomicas",
                table: "RegioesAnatomicas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImagemUlcera",
                table: "ImagemUlcera",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExsudatoTipos",
                table: "ExsudatoTipos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Pacientes_PacienteId",
                table: "Avaliacoes",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exsudatos_ExsudatoTipos_ExsudatoId",
                table: "Exsudatos",
                column: "ExsudatoId",
                principalTable: "ExsudatoTipos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Exsudatos_Ulceras_UlceraId",
                table: "Exsudatos",
                column: "UlceraId",
                principalTable: "Ulceras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImagemUlcera_Ulceras_UlceraId",
                table: "ImagemUlcera",
                column: "UlceraId",
                principalTable: "Ulceras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegioesAnatomicas_Segmentos_SegmentoId",
                table: "RegioesAnatomicas",
                column: "SegmentoId",
                principalTable: "Segmentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Topografias_RegioesAnatomicas_RegiaoId",
                table: "Topografias",
                column: "RegiaoId",
                principalTable: "RegioesAnatomicas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Topografias_Ulceras_UlceraId",
                table: "Topografias",
                column: "UlceraId",
                principalTable: "Ulceras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ulceras_Avaliacoes_AvaliacaoId",
                table: "Ulceras",
                column: "AvaliacaoId",
                principalTable: "Avaliacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ulceras_Pacientes_PacienteId",
                table: "Ulceras",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
