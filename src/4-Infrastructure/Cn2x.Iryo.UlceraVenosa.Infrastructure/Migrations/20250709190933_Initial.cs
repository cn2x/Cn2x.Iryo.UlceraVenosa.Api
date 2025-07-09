using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
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
                name: "lateralidade",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    desativada = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lateralidade", x => x.id);
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
                name: "regiao_anatomica",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sigla = table.Column<string>(type: "text", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Desativada = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_regiao_anatomica", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "regiao_topografica_pe",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sigla = table.Column<string>(type: "text", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Desativada = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_regiao_topografica_pe", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "segmentacao",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sigla = table.Column<string>(type: "text", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Desativada = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_segmentacao", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "topografia",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    lateralidade_id = table.Column<int>(type: "integer", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    desativada = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_topografia", x => x.id);
                    table.ForeignKey(
                        name: "FK_topografia_lateralidade_lateralidade_id",
                        column: x => x.lateralidade_id,
                        principalTable: "lateralidade",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "topografia_pe",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    RegiaoTopograficaPeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_topografia_pe", x => x.id);
                    table.ForeignKey(
                        name: "FK_topografia_pe_regiao_topografica_pe_RegiaoTopograficaPeId",
                        column: x => x.RegiaoTopograficaPeId,
                        principalTable: "regiao_topografica_pe",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_topografia_pe_topografia_id",
                        column: x => x.id,
                        principalTable: "topografia",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "topografia_perna",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    SegmentacaoId = table.Column<int>(type: "integer", nullable: false),
                    RegiaoAnatomicaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_topografia_perna", x => x.id);
                    table.ForeignKey(
                        name: "FK_topografia_perna_regiao_anatomica_RegiaoAnatomicaId",
                        column: x => x.RegiaoAnatomicaId,
                        principalTable: "regiao_anatomica",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_topografia_perna_segmentacao_SegmentacaoId",
                        column: x => x.SegmentacaoId,
                        principalTable: "segmentacao",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_topografia_perna_topografia_id",
                        column: x => x.id,
                        principalTable: "topografia",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ulceras",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    paciente_id = table.Column<Guid>(type: "uuid", nullable: false),
                    topografia_id = table.Column<int>(type: "integer", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_ulceras_topografia_topografia_id",
                        column: x => x.topografia_id,
                        principalTable: "topografia",
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
                    meses_duracao = table.Column<int>(type: "integer", nullable: false),
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
                name: "ceap",
                columns: table => new
                {
                    ulcera_id = table.Column<Guid>(type: "uuid", nullable: false),
                    classe_clinica = table.Column<int>(type: "integer", nullable: false),
                    etiologia = table.Column<int>(type: "integer", nullable: false),
                    anatomia = table.Column<int>(type: "integer", nullable: false),
                    patofisiologia = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ceap", x => x.ulcera_id);
                    table.ForeignKey(
                        name: "FK_ceap_ulceras_ulcera_id",
                        column: x => x.ulcera_id,
                        principalTable: "ulceras",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "caracteristicas",
                columns: table => new
                {
                    avaliacao_ferida_id = table.Column<Guid>(type: "uuid", nullable: false),
                    bordas_definidas = table.Column<bool>(type: "boolean", nullable: false),
                    tecido_granulacao = table.Column<bool>(type: "boolean", nullable: false),
                    necrose = table.Column<bool>(type: "boolean", nullable: false),
                    odor_fetido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_caracteristicas", x => x.avaliacao_ferida_id);
                    table.ForeignKey(
                        name: "FK_caracteristicas_avaliacoes_ulcera_avaliacao_ferida_id",
                        column: x => x.avaliacao_ferida_id,
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
                name: "imagens_avaliacao_ulcera",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    avaliacao_ulcera_id = table.Column<Guid>(type: "uuid", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    desativada = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_imagem_avaliacao_ulcera", x => x.id);
                    table.ForeignKey(
                        name: "FK_imagens_avaliacao_ulcera_avaliacoes_ulcera_avaliacao_ulcera~",
                        column: x => x.avaliacao_ulcera_id,
                        principalTable: "avaliacoes_ulcera",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "medidas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    avaliacao_ulcera_id = table.Column<Guid>(type: "uuid", nullable: false),
                    comprimento = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    largura = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    profundidade = table.Column<decimal>(type: "numeric(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_medidas", x => x.id);
                    table.ForeignKey(
                        name: "FK_medidas_avaliacoes_ulcera_avaliacao_ulcera_id",
                        column: x => x.avaliacao_ulcera_id,
                        principalTable: "avaliacoes_ulcera",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sinais_inflamatorios",
                columns: table => new
                {
                    avaliacao_ferida_id = table.Column<Guid>(type: "uuid", nullable: false),
                    eritema = table.Column<bool>(type: "boolean", nullable: false),
                    calor = table.Column<bool>(type: "boolean", nullable: false),
                    rubor = table.Column<bool>(type: "boolean", nullable: false),
                    edema = table.Column<bool>(type: "boolean", nullable: false),
                    dor = table.Column<bool>(type: "boolean", nullable: false),
                    perda_de_funcao = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sinais_inflamatorios", x => x.avaliacao_ferida_id);
                    table.ForeignKey(
                        name: "FK_sinais_inflamatorios_avaliacoes_ulcera_avaliacao_ferida_id",
                        column: x => x.avaliacao_ferida_id,
                        principalTable: "avaliacoes_ulcera",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "imagem",
                columns: table => new
                {
                    ImagemAvaliacaoUlceraId = table.Column<Guid>(type: "uuid", nullable: false),
                    content_type = table.Column<string>(type: "text", nullable: false),
                    tamanho_bytes = table.Column<long>(type: "bigint", nullable: false),
                    data_captura = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_imagem", x => x.ImagemAvaliacaoUlceraId);
                    table.ForeignKey(
                        name: "FK_imagem_imagens_avaliacao_ulcera_ImagemAvaliacaoUlceraId",
                        column: x => x.ImagemAvaliacaoUlceraId,
                        principalTable: "imagens_avaliacao_ulcera",
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
                table: "regiao_anatomica",
                columns: new[] { "id", "AtualizadoEm", "CriadoEm", "Desativada", "descricao", "sigla" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), false, "Medial", "M" },
                    { 2, null, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), false, "Lateral", "L" },
                    { 3, null, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), false, "Anterior", "A" },
                    { 4, null, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), false, "Posterior", "P" },
                    { 5, null, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), false, "Anteromedial", "AM" },
                    { 6, null, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), false, "Posterolateral", "PL" }
                });

            migrationBuilder.InsertData(
                table: "regiao_topografica_pe",
                columns: new[] { "id", "AtualizadoEm", "CriadoEm", "Desativada", "descricao", "sigla" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), false, "Dorsal", "DOR" },
                    { 2, null, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), false, "Plantar", "PLA" },
                    { 3, null, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), false, "Calcâneo", "CAL" },
                    { 4, null, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), false, "Mediopé", "MED" },
                    { 5, null, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), false, "Antepé", "ANT" },
                    { 6, null, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), false, "Halux", "HAL" },
                    { 7, null, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), false, "Lateral", "LAT" },
                    { 8, null, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), false, "Medial", "MEDL" },
                    { 9, null, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), false, "Malelo Medial", "MMED" },
                    { 10, null, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), false, "Malelo Lateral", "MLAT" }
                });

            migrationBuilder.InsertData(
                table: "segmentacao",
                columns: new[] { "id", "AtualizadoEm", "CriadoEm", "Desativada", "descricao", "sigla" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), false, "Da fossa poplítea até ~2/3 da altura da perna", "TS" },
                    { 2, null, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), false, "Da porção média até cerca de 1/3 acima do maléolo", "TM" },
                    { 3, null, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), false, "Do final do médio até os maléolos (região do tornozelo)", "TI" }
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
                name: "ix_imagem_avaliacao_ulcera_avaliacao_ulcera_id",
                table: "imagens_avaliacao_ulcera",
                column: "avaliacao_ulcera_id");

            migrationBuilder.CreateIndex(
                name: "ix_medidas_avaliacao_ulcera_id",
                table: "medidas",
                column: "avaliacao_ulcera_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pacientes_cpf",
                table: "pacientes",
                column: "cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_topografia_lateralidade_id",
                table: "topografia",
                column: "lateralidade_id");

            migrationBuilder.CreateIndex(
                name: "IX_topografia_pe_RegiaoTopograficaPeId",
                table: "topografia_pe",
                column: "RegiaoTopograficaPeId");

            migrationBuilder.CreateIndex(
                name: "IX_topografia_perna_RegiaoAnatomicaId",
                table: "topografia_perna",
                column: "RegiaoAnatomicaId");

            migrationBuilder.CreateIndex(
                name: "IX_topografia_perna_SegmentacaoId",
                table: "topografia_perna",
                column: "SegmentacaoId");

            migrationBuilder.CreateIndex(
                name: "ix_ulceras_paciente_id",
                table: "ulceras",
                column: "paciente_id");

            migrationBuilder.CreateIndex(
                name: "ix_ulceras_topografia_id",
                table: "ulceras",
                column: "topografia_id");
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
                name: "imagem");

            migrationBuilder.DropTable(
                name: "medidas");

            migrationBuilder.DropTable(
                name: "sinais_inflamatorios");

            migrationBuilder.DropTable(
                name: "topografia_pe");

            migrationBuilder.DropTable(
                name: "topografia_perna");

            migrationBuilder.DropTable(
                name: "exsudatos");

            migrationBuilder.DropTable(
                name: "imagens_avaliacao_ulcera");

            migrationBuilder.DropTable(
                name: "regiao_topografica_pe");

            migrationBuilder.DropTable(
                name: "regiao_anatomica");

            migrationBuilder.DropTable(
                name: "segmentacao");

            migrationBuilder.DropTable(
                name: "avaliacoes_ulcera");

            migrationBuilder.DropTable(
                name: "ulceras");

            migrationBuilder.DropTable(
                name: "pacientes");

            migrationBuilder.DropTable(
                name: "topografia");

            migrationBuilder.DropTable(
                name: "lateralidade");
        }
    }
}
