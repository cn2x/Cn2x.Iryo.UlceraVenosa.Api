using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAllTablesToSnakeCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_exsudatos_exsudato_tipos_exsudato_id",
                table: "exsudatos");

            migrationBuilder.DropForeignKey(
                name: "FK_exsudatos_ulceras_ulcera_id",
                table: "exsudatos");

            migrationBuilder.DropTable(
                name: "exsudato_tipos");

            migrationBuilder.DropPrimaryKey(
                name: "pk_exsudatos",
                table: "exsudatos");

            migrationBuilder.DropIndex(
                name: "ix_exsudatos_exsudato_id",
                table: "exsudatos");

            migrationBuilder.DropIndex(
                name: "ix_exsudatos_ulcera_id",
                table: "exsudatos");

            migrationBuilder.DropColumn(
                name: "exsudato_id",
                table: "exsudatos");

            migrationBuilder.DropColumn(
                name: "ulcera_id",
                table: "exsudatos");

            migrationBuilder.RenameColumn(
                name: "Lado",
                table: "topografias",
                newName: "lado");

            migrationBuilder.RenameIndex(
                name: "IX_topografias_ulcera_id_regiao_id_Lado",
                table: "topografias",
                newName: "IX_topografias_ulcera_id_regiao_id_lado");

            migrationBuilder.RenameColumn(
                name: "Rubor",
                table: "sinais_inflamatorios",
                newName: "rubor");

            migrationBuilder.RenameColumn(
                name: "Eritema",
                table: "sinais_inflamatorios",
                newName: "eritema");

            migrationBuilder.RenameColumn(
                name: "Edema",
                table: "sinais_inflamatorios",
                newName: "edema");

            migrationBuilder.RenameColumn(
                name: "Dor",
                table: "sinais_inflamatorios",
                newName: "dor");

            migrationBuilder.RenameColumn(
                name: "Calor",
                table: "sinais_inflamatorios",
                newName: "calor");

            migrationBuilder.RenameColumn(
                name: "PerdadeFuncao",
                table: "sinais_inflamatorios",
                newName: "perda_de_funcao");

            migrationBuilder.RenameColumn(
                name: "Patofisiologia",
                table: "ceap",
                newName: "patofisiologia");

            migrationBuilder.RenameColumn(
                name: "Etiologia",
                table: "ceap",
                newName: "etiologia");

            migrationBuilder.RenameColumn(
                name: "Anatomia",
                table: "ceap",
                newName: "anatomia");

            migrationBuilder.RenameColumn(
                name: "ClasseClinica",
                table: "ceap",
                newName: "classe_clinica");

            migrationBuilder.RenameColumn(
                name: "Necrose",
                table: "caracteristicas",
                newName: "necrose");

            migrationBuilder.RenameColumn(
                name: "TecidoGranulacao",
                table: "caracteristicas",
                newName: "tecido_granulacao");

            migrationBuilder.RenameColumn(
                name: "OdorFetido",
                table: "caracteristicas",
                newName: "odor_fetido");

            migrationBuilder.RenameColumn(
                name: "BordasDefinidas",
                table: "caracteristicas",
                newName: "bordas_definidas");

            migrationBuilder.AlterColumn<string>(
                name: "descricao",
                table: "exsudatos",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AddPrimaryKey(
                name: "pk_exsudato_tipos",
                table: "exsudatos",
                column: "id");

            migrationBuilder.CreateTable(
                name: "exsudatos_da_ulcera",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ulcera_id = table.Column<Guid>(type: "uuid", nullable: false),
                    exsudato_id = table.Column<Guid>(type: "uuid", nullable: false),
                    descricao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    desativada = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exsudatos", x => x.id);
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

            migrationBuilder.CreateIndex(
                name: "ix_exsudatos_exsudato_id",
                table: "exsudatos_da_ulcera",
                column: "exsudato_id");

            migrationBuilder.CreateIndex(
                name: "ix_exsudatos_ulcera_id",
                table: "exsudatos_da_ulcera",
                column: "ulcera_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "exsudatos_da_ulcera");

            migrationBuilder.DropPrimaryKey(
                name: "pk_exsudato_tipos",
                table: "exsudatos");

            migrationBuilder.DeleteData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));

            migrationBuilder.DeleteData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"));

            migrationBuilder.DeleteData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"));

            migrationBuilder.DeleteData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.RenameColumn(
                name: "lado",
                table: "topografias",
                newName: "Lado");

            migrationBuilder.RenameIndex(
                name: "IX_topografias_ulcera_id_regiao_id_lado",
                table: "topografias",
                newName: "IX_topografias_ulcera_id_regiao_id_Lado");

            migrationBuilder.RenameColumn(
                name: "rubor",
                table: "sinais_inflamatorios",
                newName: "Rubor");

            migrationBuilder.RenameColumn(
                name: "eritema",
                table: "sinais_inflamatorios",
                newName: "Eritema");

            migrationBuilder.RenameColumn(
                name: "edema",
                table: "sinais_inflamatorios",
                newName: "Edema");

            migrationBuilder.RenameColumn(
                name: "dor",
                table: "sinais_inflamatorios",
                newName: "Dor");

            migrationBuilder.RenameColumn(
                name: "calor",
                table: "sinais_inflamatorios",
                newName: "Calor");

            migrationBuilder.RenameColumn(
                name: "perda_de_funcao",
                table: "sinais_inflamatorios",
                newName: "PerdadeFuncao");

            migrationBuilder.RenameColumn(
                name: "patofisiologia",
                table: "ceap",
                newName: "Patofisiologia");

            migrationBuilder.RenameColumn(
                name: "etiologia",
                table: "ceap",
                newName: "Etiologia");

            migrationBuilder.RenameColumn(
                name: "anatomia",
                table: "ceap",
                newName: "Anatomia");

            migrationBuilder.RenameColumn(
                name: "classe_clinica",
                table: "ceap",
                newName: "ClasseClinica");

            migrationBuilder.RenameColumn(
                name: "necrose",
                table: "caracteristicas",
                newName: "Necrose");

            migrationBuilder.RenameColumn(
                name: "tecido_granulacao",
                table: "caracteristicas",
                newName: "TecidoGranulacao");

            migrationBuilder.RenameColumn(
                name: "odor_fetido",
                table: "caracteristicas",
                newName: "OdorFetido");

            migrationBuilder.RenameColumn(
                name: "bordas_definidas",
                table: "caracteristicas",
                newName: "BordasDefinidas");

            migrationBuilder.AlterColumn<string>(
                name: "descricao",
                table: "exsudatos",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<Guid>(
                name: "exsudato_id",
                table: "exsudatos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ulcera_id",
                table: "exsudatos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "pk_exsudatos",
                table: "exsudatos",
                column: "id");

            migrationBuilder.CreateTable(
                name: "exsudato_tipos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    desativada = table.Column<bool>(type: "boolean", nullable: false),
                    descricao = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exsudato_tipos", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "exsudato_tipos",
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

            migrationBuilder.CreateIndex(
                name: "ix_exsudatos_exsudato_id",
                table: "exsudatos",
                column: "exsudato_id");

            migrationBuilder.CreateIndex(
                name: "ix_exsudatos_ulcera_id",
                table: "exsudatos",
                column: "ulcera_id");

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
        }
    }
}
