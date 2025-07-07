using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "exsudatos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    descricao = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    desativada = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exsudato_tipos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pacientes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    cpf = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    desativada = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pacientes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "segmentos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    descricao = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    desativada = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_segmentos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ulceras",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    paciente_id = table.Column<Guid>(type: "uuid", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    desativada = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ulceras", x => x.id);
                    table.ForeignKey(
                        name: "FK_ulceras_pacientes_paciente_id",
                        column: x => x.paciente_id,
                        principalTable: "pacientes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "regioes_anatomicas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    segmento_id = table.Column<Guid>(type: "uuid", nullable: false),
                    limites = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    desativada = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_regioes_anatomicas", x => x.id);
                    table.ForeignKey(
                        name: "FK_regioes_anatomicas_segmentos_segmento_id",
                        column: x => x.segmento_id,
                        principalTable: "segmentos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "avaliacoes_ulcera",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ulcera_id = table.Column<Guid>(type: "uuid", nullable: false),
                    data_avaliacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    duracao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    desativada = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_avaliacoes_ulcera", x => x.id);
                    table.ForeignKey(
                        name: "FK_avaliacoes_ulcera_ulceras_ulcera_id",
                        column: x => x.ulcera_id,
                        principalTable: "ulceras",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exsudatos_da_ulcera",
                columns: table => new
                {
                    ulcera_id = table.Column<Guid>(type: "uuid", nullable: false),
                    exsudato_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exsudatos_da_ulcera", x => new { x.ulcera_id, x.exsudato_id });
                    table.ForeignKey(
                        name: "FK_exsudatos_da_ulcera_exsudatos_exsudato_id",
                        column: x => x.exsudato_id,
                        principalTable: "exsudatos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_exsudatos_da_ulcera_ulceras_ulcera_id",
                        column: x => x.ulcera_id,
                        principalTable: "ulceras",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "topografias",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ulcera_id = table.Column<Guid>(type: "uuid", nullable: false),
                    regiao_id = table.Column<Guid>(type: "uuid", nullable: false),
                    lado = table.Column<int>(type: "integer", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    desativada = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_topografias", x => x.id);
                    table.ForeignKey(
                        name: "FK_topografias_regioes_anatomicas_regiao_id",
                        column: x => x.regiao_id,
                        principalTable: "regioes_anatomicas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_topografias_ulceras_ulcera_id",
                        column: x => x.ulcera_id,
                        principalTable: "ulceras",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "caracteristicas",
                columns: table => new
                {
                    AvaliacaoUlceraId = table.Column<Guid>(type: "uuid", nullable: false),
                    bordas_definidas = table.Column<bool>(type: "boolean", nullable: false),
                    tecido_granulacao = table.Column<bool>(type: "boolean", nullable: false),
                    necrose = table.Column<bool>(type: "boolean", nullable: false),
                    odor_fetido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_caracteristicas", x => x.AvaliacaoUlceraId);
                    table.ForeignKey(
                        name: "FK_caracteristicas_avaliacoes_ulcera_AvaliacaoUlceraId",
                        column: x => x.AvaliacaoUlceraId,
                        principalTable: "avaliacoes_ulcera",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ceap",
                columns: table => new
                {
                    AvaliacaoUlceraId = table.Column<Guid>(type: "uuid", nullable: false),
                    classe_clinica = table.Column<int>(type: "integer", nullable: false),
                    etiologia = table.Column<int>(type: "integer", nullable: false),
                    anatomia = table.Column<int>(type: "integer", nullable: false),
                    patofisiologia = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ceap", x => x.AvaliacaoUlceraId);
                    table.ForeignKey(
                        name: "FK_ceap_avaliacoes_ulcera_AvaliacaoUlceraId",
                        column: x => x.AvaliacaoUlceraId,
                        principalTable: "avaliacoes_ulcera",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exsudatos_avaliacao",
                columns: table => new
                {
                    avaliacao_ulcera_id = table.Column<Guid>(type: "uuid", nullable: false),
                    exsudato_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exsudatos_avaliacao", x => new { x.avaliacao_ulcera_id, x.exsudato_id });
                    table.ForeignKey(
                        name: "FK_exsudatos_avaliacao_avaliacoes_ulcera_avaliacao_ulcera_id",
                        column: x => x.avaliacao_ulcera_id,
                        principalTable: "avaliacoes_ulcera",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_exsudatos_avaliacao_exsudatos_exsudato_id",
                        column: x => x.exsudato_id,
                        principalTable: "exsudatos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "imagem_ulcera",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ulcera_id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome_arquivo = table.Column<string>(type: "text", nullable: false),
                    caminho_arquivo = table.Column<string>(type: "text", nullable: false),
                    content_type = table.Column<string>(type: "text", nullable: false),
                    tamanho_bytes = table.Column<long>(type: "bigint", nullable: false),
                    data_captura = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: true),
                    observacoes = table.Column<string>(type: "text", nullable: true),
                    eh_imagem_principal = table.Column<bool>(type: "boolean", nullable: false),
                    ordem_exibicao = table.Column<int>(type: "integer", nullable: false),
                    UlceraId1 = table.Column<Guid>(type: "uuid", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    desativada = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_imagem_ulcera", x => x.id);
                    table.ForeignKey(
                        name: "FK_imagem_ulcera_avaliacoes_ulcera_ulcera_id",
                        column: x => x.ulcera_id,
                        principalTable: "avaliacoes_ulcera",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_imagem_ulcera_ulceras_UlceraId1",
                        column: x => x.UlceraId1,
                        principalTable: "ulceras",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "medidas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ulcera_id = table.Column<Guid>(type: "uuid", nullable: false),
                    comprimento = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    largura = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    profundidade = table.Column<decimal>(type: "numeric(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_medidas", x => x.id);
                    table.ForeignKey(
                        name: "FK_medidas_avaliacoes_ulcera_ulcera_id",
                        column: x => x.ulcera_id,
                        principalTable: "avaliacoes_ulcera",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_medidas_ulceras_ulcera_id",
                        column: x => x.ulcera_id,
                        principalTable: "ulceras",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sinais_inflamatorios",
                columns: table => new
                {
                    AvaliacaoUlceraId = table.Column<Guid>(type: "uuid", nullable: false),
                    eritema = table.Column<bool>(type: "boolean", nullable: false),
                    calor = table.Column<bool>(type: "boolean", nullable: false),
                    rubor = table.Column<bool>(type: "boolean", nullable: false),
                    edema = table.Column<bool>(type: "boolean", nullable: false),
                    dor = table.Column<bool>(type: "boolean", nullable: false),
                    perda_de_funcao = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sinais_inflamatorios", x => x.AvaliacaoUlceraId);
                    table.ForeignKey(
                        name: "FK_sinais_inflamatorios_avaliacoes_ulcera_AvaliacaoUlceraId",
                        column: x => x.AvaliacaoUlceraId,
                        principalTable: "avaliacoes_ulcera",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "exsudatos",
                columns: new[] { "id", "atualizado_em", "criado_em", "desativada", "descricao" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), null, new DateTime(2025, 6, 28, 17, 32, 53, 0, DateTimeKind.Utc), false, "Seroso - Transparente ou levemente amarelo, aquoso, fluido. Indicação: Fase inflamatória leve ou cicatrização. Conduta: Monitorar, manter hidratação da ferida." },
                    { new Guid("22222222-2222-2222-2222-222222222222"), null, new DateTime(2025, 6, 28, 17, 32, 53, 0, DateTimeKind.Utc), false, "Serossanguinolento - Rosa claro, diluído com sangue, levemente viscoso. Indicação: Trauma leve ou início de granulação. Conduta: Avaliar trauma, proteger bordas." },
                    { new Guid("33333333-3333-3333-3333-333333333333"), null, new DateTime(2025, 6, 28, 17, 32, 53, 0, DateTimeKind.Utc), false, "Sanguinolento - Vermelho vivo, líquido a viscoso. Indicação: Sangramento ativo ou lesão capilar. Conduta: Estancar, avaliar necessidade de sutura." },
                    { new Guid("44444444-4444-4444-4444-444444444444"), null, new DateTime(2025, 6, 28, 17, 32, 53, 0, DateTimeKind.Utc), false, "Hemorrágico - Vermelho escuro ou vivo, espesso, com coágulos. Indicação: Hemorragia arterial ou venosa local. Conduta: Urgência médica, hemostasia." },
                    { new Guid("55555555-5555-5555-5555-555555555555"), null, new DateTime(2025, 6, 28, 17, 32, 53, 0, DateTimeKind.Utc), false, "Purulento - Amarelo, esverdeado ou acastanhado, espesso, fétido. Indicação: Infecção bacteriana ativa. Conduta: Cultura, antibioticoterapia, limpeza." },
                    { new Guid("66666666-6666-6666-6666-666666666666"), null, new DateTime(2025, 6, 28, 17, 32, 53, 0, DateTimeKind.Utc), false, "Fibrinoso - Esbranquiçado ou amarelado, gelatinoso, filamentoso. Indicação: Presença de fibrina, biofilme. Conduta: Desbridamento, controle da umidade." },
                    { new Guid("77777777-7777-7777-7777-777777777777"), null, new DateTime(2025, 6, 28, 17, 32, 53, 0, DateTimeKind.Utc), false, "Catarral - Esbranquiçado e mucoide, viscoso. Indicação: Presente em áreas mucosas ou com inflamação leve. Conduta: Raro em úlceras venosas, observar." },
                    { new Guid("88888888-8888-8888-8888-888888888888"), null, new DateTime(2025, 6, 28, 17, 32, 53, 0, DateTimeKind.Utc), false, "Necrótico - Marrom, cinza ou preto, espesso, seco ou úmido. Indicação: Presença de necrose tecidual. Conduta: Desbridamento enzimático ou cirúrgico." },
                    { new Guid("99999999-9999-9999-9999-999999999999"), null, new DateTime(2025, 6, 28, 17, 32, 53, 0, DateTimeKind.Utc), false, "Putrilaginoso - Cinza-esverdeado, muito espesso, pegajoso, fétido. Indicação: Infecção crítica, tecido desvitalizado. Conduta: Ação rápida: desbridamento + antibiótico." },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), null, new DateTime(2025, 6, 28, 17, 32, 53, 0, DateTimeKind.Utc), false, "Hiperexsudativo - Variável, muito abundante. Indicação: Descompensação venosa, linforreia, infecção. Conduta: Curativos superabsorventes, compressão." }
                });

            migrationBuilder.InsertData(
                table: "segmentos",
                columns: new[] { "id", "atualizado_em", "criado_em", "desativada", "descricao", "nome" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), null, new DateTime(2025, 6, 28, 18, 0, 0, 0, DateTimeKind.Utc), false, "Ao redor dos maléolos (interno e externo), especialmente o maleolo medial (tíbia). Local mais comum de úlcera venosa. Associada à hipertensão venosa crônica.", "Região maleolar ou perimaleolar" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), null, new DateTime(2025, 6, 28, 18, 0, 0, 0, DateTimeKind.Utc), false, "Entre o maléolo e a metade da perna. Região de drenagem venosa crítica. Úlceras nesta zona indicam comprometimento venoso avançado.", "Terço inferior da perna" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), null, new DateTime(2025, 6, 28, 18, 0, 0, 0, DateTimeKind.Utc), false, "Da metade até abaixo do joelho. Menos comum para úlceras venosas. Úlceras aqui sugerem causas mistas (venosa + arterial ou vasculite).", "Terço médio e superior da perna" }
                });

            migrationBuilder.InsertData(
                table: "regioes_anatomicas",
                columns: new[] { "id", "atualizado_em", "criado_em", "desativada", "limites", "segmento_id" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), null, new DateTime(2025, 6, 28, 18, 30, 0, 0, DateTimeKind.Utc), false, "Região maleolar - Ao redor do maléolo medial e lateral (tornozelo). Frequência: 10%", new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), null, new DateTime(2025, 6, 28, 18, 30, 0, 0, DateTimeKind.Utc), false, "Terço inferior da perna - Entre a base do tornozelo e a metade da perna. Frequência: 73%", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("33333333-3333-3333-3333-333333333333"), null, new DateTime(2025, 6, 28, 18, 30, 0, 0, DateTimeKind.Utc), false, "Terço médio/superior da perna - Da metade da perna até a fossa poplítea (abaixo do joelho). Frequência: 0%", new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("44444444-4444-4444-4444-444444444444"), null, new DateTime(2025, 6, 28, 18, 30, 0, 0, DateTimeKind.Utc), false, "Maleolar + Terço inferior - Úlcera extensa envolvendo tornozelo e porção inferior da perna. Frequência: 15%", new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("55555555-5555-5555-5555-555555555555"), null, new DateTime(2025, 6, 28, 18, 30, 0, 0, DateTimeKind.Utc), false, "Terço inferior + Terço médio/superior - Lesões ascendentes ou disseminadas, raras em úlceras puramente venosas. Frequência: 2%", new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.CreateIndex(
                name: "ix_avaliacoes_ulcera_ulcera_id",
                table: "avaliacoes_ulcera",
                column: "ulcera_id");

            migrationBuilder.CreateIndex(
                name: "ix_exsudatos_avaliacao_exsudato_id",
                table: "exsudatos_avaliacao",
                column: "exsudato_id");

            migrationBuilder.CreateIndex(
                name: "ix_exsudatos_exsudato_id",
                table: "exsudatos_da_ulcera",
                column: "exsudato_id");

            migrationBuilder.CreateIndex(
                name: "ix_imagem_ulcera_ulcera_id",
                table: "imagem_ulcera",
                column: "ulcera_id");

            migrationBuilder.CreateIndex(
                name: "IX_imagem_ulcera_UlceraId1",
                table: "imagem_ulcera",
                column: "UlceraId1");

            migrationBuilder.CreateIndex(
                name: "ix_medidas_ulcera_id",
                table: "medidas",
                column: "ulcera_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pacientes_cpf",
                table: "pacientes",
                column: "cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_regioes_anatomicas_segmento_id",
                table: "regioes_anatomicas",
                column: "segmento_id");

            migrationBuilder.CreateIndex(
                name: "ix_topografias_regiao_id",
                table: "topografias",
                column: "regiao_id");

            migrationBuilder.CreateIndex(
                name: "IX_topografias_ulcera_id_regiao_id_lado",
                table: "topografias",
                columns: new[] { "ulcera_id", "regiao_id", "lado" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ulceras_paciente_id",
                table: "ulceras",
                column: "paciente_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "caracteristicas");

            migrationBuilder.DropTable(
                name: "ceap");

            migrationBuilder.DropTable(
                name: "exsudatos_avaliacao");

            migrationBuilder.DropTable(
                name: "exsudatos_da_ulcera");

            migrationBuilder.DropTable(
                name: "imagem_ulcera");

            migrationBuilder.DropTable(
                name: "medidas");

            migrationBuilder.DropTable(
                name: "sinais_inflamatorios");

            migrationBuilder.DropTable(
                name: "topografias");

            migrationBuilder.DropTable(
                name: "exsudatos");

            migrationBuilder.DropTable(
                name: "avaliacoes_ulcera");

            migrationBuilder.DropTable(
                name: "regioes_anatomicas");

            migrationBuilder.DropTable(
                name: "ulceras");

            migrationBuilder.DropTable(
                name: "segmentos");

            migrationBuilder.DropTable(
                name: "pacientes");
        }
    }
}
