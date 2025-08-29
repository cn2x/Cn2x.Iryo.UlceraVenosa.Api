using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNomeToExsudato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "descricao",
                table: "exsudatos",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "nome",
                table: "exsudatos",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "descricao", "nome" },
                values: new object[] { "Transparente ou levemente amarelo, aquoso, fluido. Indicação: Fase inflamatória leve ou cicatrização. Conduta: Monitorar, manter hidratação da ferida.", "Seroso" });

            migrationBuilder.UpdateData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "descricao", "nome" },
                values: new object[] { "Rosa claro, diluído com sangue, levemente viscoso. Indicação: Trauma leve ou início de granulação. Conduta: Avaliar trauma, proteger bordas.", "Serossanguinolento" });

            migrationBuilder.UpdateData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "descricao", "nome" },
                values: new object[] { "Vermelho vivo, líquido a viscoso. Indicação: Sangramento ativo ou lesão capilar. Conduta: Estancar, avaliar necessidade de sutura.", "Sanguinolento" });

            migrationBuilder.UpdateData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "descricao", "nome" },
                values: new object[] { "Vermelho escuro ou vivo, espesso, com coágulos. Indicação: Hemorragia arterial ou venosa local. Conduta: Urgência médica, hemostasia.", "Hemorrágico" });

            migrationBuilder.UpdateData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "descricao", "nome" },
                values: new object[] { "Amarelo, esverdeado ou acastanhado, espesso, fétido. Indicação: Infecção bacteriana ativa. Conduta: Cultura, antibioticoterapia, limpeza.", "Purulento" });

            migrationBuilder.UpdateData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                columns: new[] { "descricao", "nome" },
                values: new object[] { "Esbranquiçado ou amarelado, gelatinoso, filamentoso. Indicação: Presença de fibrina, biofilme. Conduta: Desbridamento, controle da umidade.", "Fibrinoso" });

            migrationBuilder.UpdateData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                columns: new[] { "descricao", "nome" },
                values: new object[] { "Esbranquiçado e mucoide, viscoso. Indicação: Presente em áreas mucosas ou com inflamação leve. Conduta: Raro em úlceras venosas, observar.", "Catarral" });

            migrationBuilder.UpdateData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "descricao", "nome" },
                values: new object[] { "Marrom, cinza ou preto, espesso, seco ou úmido. Indicação: Presença de necrose tecidual. Conduta: Desbridamento enzimático ou cirúrgico.", "Necrótico" });

            migrationBuilder.UpdateData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                columns: new[] { "descricao", "nome" },
                values: new object[] { "Cinza-esverdeado, muito espesso, pegajoso, fétido. Indicação: Infecção crítica, tecido desvitalizado. Conduta: Ação rápida: desbridamento + antibiótico.", "Putrilaginoso" });

            migrationBuilder.UpdateData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "descricao", "nome" },
                values: new object[] { "Variável, muito abundante. Indicação: Descompensação venosa, linforreia, infecção. Conduta: Curativos superabsorventes, compressão.", "Hiperexsudativo" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nome",
                table: "exsudatos");

            migrationBuilder.AlterColumn<string>(
                name: "descricao",
                table: "exsudatos",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.UpdateData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "descricao",
                value: "Seroso - Transparente ou levemente amarelo, aquoso, fluido. Indicação: Fase inflamatória leve ou cicatrização. Conduta: Monitorar, manter hidratação da ferida.");

            migrationBuilder.UpdateData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "descricao",
                value: "Serossanguinolento - Rosa claro, diluído com sangue, levemente viscoso. Indicação: Trauma leve ou início de granulação. Conduta: Avaliar trauma, proteger bordas.");

            migrationBuilder.UpdateData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "descricao",
                value: "Sanguinolento - Vermelho vivo, líquido a viscoso. Indicação: Sangramento ativo ou lesão capilar. Conduta: Estancar, avaliar necessidade de sutura.");

            migrationBuilder.UpdateData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "descricao",
                value: "Hemorrágico - Vermelho escuro ou vivo, espesso, com coágulos. Indicação: Hemorragia arterial ou venosa local. Conduta: Urgência médica, hemostasia.");

            migrationBuilder.UpdateData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "descricao",
                value: "Purulento - Amarelo, esverdeado ou acastanhado, espesso, fétido. Indicação: Infecção bacteriana ativa. Conduta: Cultura, antibioticoterapia, limpeza.");

            migrationBuilder.UpdateData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "descricao",
                value: "Fibrinoso - Esbranquiçado ou amarelado, gelatinoso, filamentoso. Indicação: Presença de fibrina, biofilme. Conduta: Desbridamento, controle da umidade.");

            migrationBuilder.UpdateData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "descricao",
                value: "Catarral - Esbranquiçado e mucoide, viscoso. Indicação: Presente em áreas mucosas ou com inflamação leve. Conduta: Raro em úlceras venosas, observar.");

            migrationBuilder.UpdateData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "descricao",
                value: "Necrótico - Marrom, cinza ou preto, espesso, seco ou úmido. Indicação: Presença de necrose tecidual. Conduta: Desbridamento enzimático ou cirúrgico.");

            migrationBuilder.UpdateData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                column: "descricao",
                value: "Putrilaginoso - Cinza-esverdeado, muito espesso, pegajoso, fétido. Indicação: Infecção crítica, tecido desvitalizado. Conduta: Ação rápida: desbridamento + antibiótico.");

            migrationBuilder.UpdateData(
                table: "exsudatos",
                keyColumn: "id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "descricao",
                value: "Hiperexsudativo - Variável, muito abundante. Indicação: Descompensação venosa, linforreia, infecção. Conduta: Curativos superabsorventes, compressão.");
        }
    }
}
